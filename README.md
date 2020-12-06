# Sqreen dispatcher backend draft

## Structure
- 1 web project wihout suffix just called SqreenDispatcher
- 1 services project : SqreenDispatcher.Services
- tests projects suffixed by tests
> organized through virtual solution folders in visual studio: src and tests folders. 

The IOC is defined in class Startup.cs as per design in Asp.net Core.
It is overidden in some of the tests

**Main url is https://mydomain/sqreen**

Settings are defined in appsettings.json, and other appsettings.[env].json. I used Development environment, through a environment variable which is ASPNETCORE_ENVIRONMENT = Development.
It is overidden in the tests. 

There are 3 targets for now: email (Geeklearning library with handlebars templating), log files (Serilog framework), database (sqllite for demo purpose and direct access instead of an ORM like entity framework). 

### Architecture

1. **Startup.cs** runs first. It defines all IOC. Depending on the configuration, it will register different targets in the IOC container. 
2. Requests arrive to the **SqreenController.cs** Alert(messages) method, which in turn, call the services layer with the deserialized messages. 
3. The **Dispatcher.cs** comes into play. He is responsible for dispatching the messages to registered targets in configuration. His constructor takes all implementations of ITarget available in the IOC container. It doesn't have any knowledge as to which target or which implementation is in its list. 
4. All targets receive parallely the messages and treat them according to their specific implementations and configurations. 

>One could add any number of targets by deriving from the type ITarget and registering a key (in TargetsConsts) with a matching type to instantiate. 

The library geeklearning allow to configure mockup recipients, templates, different storage options (file system, azure storage..), different email providers (smtp, sendgrid..), here for demo purposes, we use the file system storage with email templates being in the folder EmailTempplates.

## Authentication
[AuthorizeSqreenAttribute] class is decorating the main controller in Controllers folder. 
But the logic in the Services library: Hmac\Verifier.cs

## Tests
we use framework Mock to mock interface and to verify how many times methods have been called with which parameters etc.

2 types of tests:
Tests can choose to use appsettings.Tests.json
#### unit tests: 
essentially for the authenticator verifier
#### integration tests: 
replacing targets in IOC. One could think of really testing the targets themselves, or their equivalent in memory. 

*NB: for demo purposes, all properties of the sent json haven't been treated in all targets. We would just need to add them, and format them according to the target.
Likewise, the app doesn't have a UI yet, its main purpose is to dispatch messages.*
