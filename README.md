# Hu.Challenge.Gbso.Dev Deploying Services with Docker Compose

## Requirements

1. **Docker**: Ensure Docker is installed on your machine. You can install Docker from [here](https://www.docker.com/get-started).
2. **Docker Compose**: Docker Compose comes with Docker Desktop on Windows and macOS. If you're on Linux, follow the instructions to install Docker Compose from [here](https://docs.docker.com/compose/install/).
3. **Configuration Files**: Make sure you have the following files in your project directory:
   - `.env`: Contains the environment variables used by the services.
   - `docker-compose.yml`: The main file to define Docker services and networks.
   - `docker-compose.override.yml`: Optional file to override specific configurations in the development environment.

## Configuration Files

### .env

This file defines the environment variables needed for the services, such as database and RabbitMQ credentials, as well as configurations for specific workers. An example of the variables you can define:

#### PostgresSql
```DATABASE_SERVER=challenge.database```

```POSTGRES_USER=challenge-user```

```POSTGRES_PASSWORD=challenge-pw```

```POSTGRES_DB=challenge-db```


#### RabbitMQ
```RABBITMQ_HOSTNAME=challenge.rabbitmq```

```RABBITMQ_USERNAME=challenge-user```

```RABBITMQ_PASSWORD=challenge-pw```


#### SendGrid
```SENDGRID_APIKEY=YOUR_SENDGRID_API_KEY```

```SENDGRID_FROMEMAIL=gb.so.dev@outlook.com```

```SENDGRID_FROMNAME=Flights Areline```


#### Challenge.Outbox.PublisherWorker
```OUTBOX_PUBLISHERWORKER_NUMBEROFRETRIES=1```

```OUTBOX_PUBLISHERWORKER_RETRYDELAY=0.00:01:00```

```OUTBOX_PUBLISHERWORKER_EXECUTIONINTERVAL=0.00:05:00```


#### Challenge.Broker.NotificationWorker
```BROKER_NOTIFICATIONWORKER_NUMBEROFRETRIES=5```

```BROKER_NOTIFICATIONWORKER_RETRYDELAY=0.00:01:00```

```BROKER_NOTIFICATIONWORKER_EXECUTIONINTERVAL=0.00:05:00```


#### Challenge.Email.NotificationWorker
```EMAIL_NOTIFICATIONWORKER_NUMBEROFRETRIES=2```

```EMAIL_NOTIFICATIONWORKER_RETRYDELAY=0.00:01:00```

```EMAIL_NOTIFICATIONWORKER_EXECUTIONINTERVAL=0.00:01:00```

# Start the services
docker-compose up

# Start the services in detached mode (in the background)
docker-compose up -d

# Rebuild the images and start the services
docker-compose up --build

# Stop and remove the containers, networks, and volumes
docker-compose down

# Override environment variables when starting the services
POSTGRES_DB=override-database POSTGRES_USER=override-user docker-compose up