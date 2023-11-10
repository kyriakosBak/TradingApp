output "instance_id" {
  description = "ID of EC2 instance"
  value = aws_instance.trading_app.id
}

output "instance_public_ip" {
  description = "Publi IP of EC2 instance"
  value = aws_instance.trading_app.public_ip
}
