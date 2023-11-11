#!/bin/bash
sudo service docker start
sudo docker run -d -p 80:80 rebelor/tradingapp:latest

