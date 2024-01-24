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

data "aws_ami" "ubuntu" {
  most_recent = true
  owners      = ["amazon"]

  filter {
    name   = "name"
    values = ["ubuntu*amd64-server*"]
  }
}

data "aws_availability_zones" "available" {
  state = "available"
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

# Create an IAM role for the EC2 instance
resource "aws_iam_role" "ec2_dynamodb_role" {
  name = "ec2_dynamodb_role"
  
  assume_role_policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Action": "sts:AssumeRole",
      "Effect": "Allow",
      "Principal": {
        "Service": "ec2.amazonaws.com"
      }
    }
  ]
}
EOF
}

resource "aws_iam_role_policy_attachment" "dunamodb_access_attachment" {
  policy_arn = "arn:aws:iam::aws:policy/AmazonDynamoDBReadOnlyAccess"
  role       = aws_iam_role.ec2_dynamodb_role.name
}

resource "aws_iam_instance_profile" "ec2_profile" {
  name = "trading_app_ec2_profile"
  role = aws_iam_role.ec2_dynamodb_role.name
}

resource "aws_instance" "trading_app" {
  ami           = data.aws_ami.ubuntu.id
  instance_type = "t2.micro"
  key_name      = "ec2key"
  subnet_id     = module.vpc.public_subnets[0]
  user_data     = file("user-data.sh")
  iam_instance_profile = aws_iam_instance_profile.ec2_profile.name

  tags = {
    Name = "trading-app"
  }
}
