docker build -t grasdu/users.api ./Users.API
docker build -t grasdu/users.logger ./Users.Logger

docker push grasdu/users.api
docker push grasdu/users.logger