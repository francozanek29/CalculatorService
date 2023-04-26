# CalculatorService

## Architecture & Components
This should be the main components of the delivered application: 

CalculatorService.Server: The main component of the application, and the one actually implementing the 'calculator service’ HTTP/REST
interface & business logic.

CalculatorService.Client: A demonstration console/command line client, capable of performing requests to the main HTTP service from CLI.

### Server Application structure
The archicture defined in this project is divided as follows:
- src: where all the code is. 
    - Bootstraper: In this project will be the start point for the application, here all the 
                   dependecies definition will be defined and the "custom" insfrastructure configuration
                   will be defined. This project has dependecies on all the other projects is also
                   the way to connect all the other application projects.
    - Core.Model: In this project we defined all the entities and all the interfaces that will be 
                  needed in the application. This projects is going to be used in all the other projects
                  so we rely on the dependecy injection definition to perform the operations and with this
                  we can assure the only knowledge that the other projects have on each other is the one
                  defined in the interfaces.
    - Core.Services: This layer is in charge of defining all the classes that will be incharge of
                     connecting the Controllers and the repository and perform all the transformation
                     needed to send the data from one layer to another.
    - Repository: This layer will be in charge of handling the data access to the repository.
    - WebApi: Is the project where all the controllers, filters, are defined in order to stablish
              the connection between the user and the repository. 
              
    Just to take into account in the Model project the entities that are defined there are those
    that are going to be used in the all application. In case of the WebApi and Integration project
    which have its own entities to handle the data, those are defined inside of each project and then
    mapping those entities into Model entities to send the information to be used inside the application.

- tests: all unit tests and integration tests needed to cover the code. Each project defined above
  has its own project test. Each project name is the same that the project name defined in the
  src folder adding ".Tests". Also the folder structure defined in each test project is the same
  than the folder structure defined in the src project definition.

## WebApi Definition

The API definition you can find it on this [link](https://app.swaggerhub.com/apis/ZANEKFRANCO_1/CalculatorService.Server/1.0.0)

## How to test the application

There are two different solution (one for each project defined at the top). You need to execute the server and then the client. When that´s working, the Client project has an interactibe menu that guides through the application execution.
