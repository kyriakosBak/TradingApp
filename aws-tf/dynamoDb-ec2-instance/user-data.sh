#!/bin/bash
sudo apt-get update -y
sudo apt-get install -y docker.io
sudo service docker start
sudo docker run -d -p 80:80 rebelor/tradingapp:latest

