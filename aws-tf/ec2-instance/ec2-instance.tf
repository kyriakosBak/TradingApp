provider "aws" {
  region = "eu-central-1"
}

resource "aws_instance" "example" {
  // Amazon Linux 2023 AMI
  ami           = "ami-0a485299eeb98b979"
  instance_type = "t2.micro"

  tags = {
    Name = "example-instance"
  }
}
