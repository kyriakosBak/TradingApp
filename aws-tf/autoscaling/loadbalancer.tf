# Load balancer
resource "aws_security_group" "trading_app_lb" {
  name   = "trading-app-lb-sg"
  count = var.use_load_balancer ? 1 : 0
  vpc_id = module.vpc.vpc_id

  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

resource "aws_lb" "trading_app_lb" {
  name               = "trading-app-lb"
  count = var.use_load_balancer ? 1 : 0
  internal           = false
  load_balancer_type = "application"
  subnets            = module.vpc.public_subnets
  security_groups    = [aws_security_group.trading_app_lb[0].id]
}

resource "aws_lb_listener" "trading_app_lb" {
  count = var.use_load_balancer ? 1 : 0
  load_balancer_arn = aws_lb.trading_app_lb[0].arn
  port              = 80
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.trading_app_lb[0].arn
  }
}

resource "aws_lb_target_group" "trading_app_lb" {
  name     = "trading-app-lb-target-group"
  count = var.use_load_balancer ? 1 : 0
  port     = 80
  protocol = "HTTP"
  vpc_id   = module.vpc.vpc_id
}

resource "aws_autoscaling_attachment" "trading_app_lb" {
  count = var.use_load_balancer ? 1 : 0

  autoscaling_group_name = aws_autoscaling_group.trading_app.id
  lb_target_group_arn   = aws_lb_target_group.trading_app_lb[0].arn
}