# TwitterStyleApplication
### It-Challenges 2016 fall, back-end final

##Demo
> - http://back-end-task.info

## Run in docker
### Pull image of application from docker hub and run on 8080 port:
> - **`docker run -p 8080:5000 itchallenges/twitter-like-app`**
> - Run localhost:8080 in the browser, if the port is busy, change `8080` in `docker run` command to any available port and run again.

### Alternative way - build from sources and run:
> - `docker build -t app .`
> - `docker run -p 8080:5000 -t app`

## Technologies used:
**Backend:** ASP.NET Core, Entity Framework Core, Automapper, MS SQL, Swagger (Auto-generated documentation for API)

**Frontend:** AngularJS, Bootstrap, JQuery, Alertify.js.