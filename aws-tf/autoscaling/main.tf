terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "5.24.0"
    }
  }
}

provider "aws" {
  region = "eu-central-1"
}

data "aws_availability_zones" "available" {
  state = "available"
}

data "aws_ami" "trading_app" {
  most_recent = true
  owners      = ["811585347462"]

  filter {
    name   = "name"
    values = ["TradingApp"]
  }
}

module "vpc" {
  source = "terraform-aws-modules/vpc/aws"

  name = "trading-app-vpc"
  cidr = "10.0.0.0/16"

  azs                     = data.aws_availability_zones.available.names
  public_subnets          = ["10.0.101.0/24", "10.0.102.0/24", "10.0.103.0/24"]
  map_public_ip_on_launch = true

  tags = {
    Terraform   = "true"
    Environment = "dev"
  }

  default_security_group_ingress = [
    {
      description = "Allow HTTP inbound traffic"
      from_port   = 80
      to_port     = 80
      protocol    = "tcp"
      cidr_blocks = "0.0.0.0/0"
    }
  ]

  default_security_group_egress = [
    {
      from_port   = 0
      to_port     = 0
      protocol    = -1
      cidr_blocks = "0.0.0.0/0"
    }
  ]
}

resource "aws_security_group" "trading_app" {
  name   = "trading-app-instance-sg"
  vpc_id = module.vpc.vpc_id
  
  ingress {
    from_port = 80
    to_port   = 80
    protocol  = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    security_groups = [ module.vpc.default_security_group_id ]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}
resource "aws_autoscaling_group" "trading_app" {
  name                 = "trading-app"
  min_size             = 1
  max_size             = 2
  desired_capacity     = 1
  vpc_zone_identifier  = module.vpc.public_subnets

  launch_template {
    id      = aws_launch_template.trading_app.id
    version = "$Latest"
  }

  health_check_type = "EC2"

  tag {
    key                 = "Name"
    value               = "Trading App"
    propagate_at_launch = true
  }
}

resource "aws_launch_template" "trading_app" {
  name_prefix   = "trading-app-launch-config"
  image_id      = data.aws_ami.trading_app.id
  instance_type = "t2.micro"
  user_data     = filebase64("user-data.sh")

  network_interfaces {
    associate_public_ip_address = var.use_load_balancer ? false : true
    security_groups             = (
      var.use_load_balancer ?
      [ aws_security_group.trading_app_lb[0].id ] :
      [ aws_security_group.trading_app.id ]
    )
  }
}
