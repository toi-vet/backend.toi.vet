Push-Location Toi.Backend
docker build -t toi-backend .
heroku container:push -a toi-backend web
heroku container:release -a toi-backend web
Pop-Location