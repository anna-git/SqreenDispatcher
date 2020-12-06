# Sqreen dispatcher backend draft

## Structure
- 1 web project wihout suffix just called SqreenDispatcher
- 1 services project 
- tests projects
> structured through virtual solution folders in visual studio 

The IOC is defined in class Startup.cs as per design in Asp.net Core.

**Main url is https://mydomain/sqreen**

Settings are defined in appsettings.json, and other appsettings.[env].json. I used Development environment, through a environment variable which is ASPNETCORE_ENVIRONMENT = Development.
It is overidden in the tests. 

There are 3 targets for now: email (Geeklearning library with handlebars templating), log files (Serilog framework), database (sqllite for demo purpose and direct access instead of an ORM like entity framework). 

### Architecture

One could add any number of targets by deriving from the type ITarget and registering a key (in TargetsConsts) with a matching type to instantiate. 

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
