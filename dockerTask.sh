docker stop kriptofeet
docker rm kriptofeet
docker image rm kriptofeet
docker build -t kriptofeet .
docker run -d -p 565:80 --name kriptofeet kriptofeet
