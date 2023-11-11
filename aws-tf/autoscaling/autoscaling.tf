terraform {
  required_providers {
    aws = {
      source = "hashicorp/aws"
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

data "aws_ami" "ubuntu" {
  most_recent = true
  owners      = ["amazon"]

  filter {
    name   = "name"
    values = ["ubuntu*amd64-server*"]
  }
}

module "vpc" {
  source = "terraform-aws-modules/vpc/aws"

  name = "trading-app-vpc"
  cidr = "10.0.0.0/16"

  azs             = data.aws_availability_zones.available.names
  public_subnets  = ["10.0.101.0/24", "10.0.102.0/24", "10.0.103.0/24"]
  map_public_ip_on_launch = true

  tags = {
    Terraform = "true"
    Environment = "dev"
  }

  default_security_group_ingress = [
    {
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

resource "aws_security_group" "trading_app_sg" {
  name   = "trading_app_sg"
  ingres {
    from_port   = 80
    security)groups

resource "aws_launch_configuration" "trading_app_launch_config" {
  name_prefix     = "trading_app_launch_config"
  image_id        = data.aws_ami.ubuntu.id
  instance_type   = "t2.micro"
  user_data       = file("user-data.sh")

  life
}

resource "aws_instance" "trading_app" {
  ami           = data.aws_ami.ubuntu.id
  instance_type = "t2.micro"
  key_name      = "ec2key"
  subnet_id     = module.vpc.public_subnets[0]

  user_data     = file("user-data.sh")

  tags = {
    Name = "trading-app"
  }
}
