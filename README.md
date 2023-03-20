# RudderstackForms - Introduction
 
This repository contains code for the RudderstackForms service which provides APIs to create different Form Templates and Sources based on those Form Templates. Link to frontend repo: [rudderstack-forms-frontend](https://github.com/shambhavi-rani/rudderstack-forms-frontend)

## Getting Started
### Prerequisites
1. The solution is built on VS2022 with the ASP.NET and web development workload
2. [MongoDB](https://www.mongodb.com/docs/manual/tutorial/install-mongodb-on-windows/)
3. [MongoDB Shell](https://www.mongodb.com/docs/mongodb-shell/install/)

### Installing
1. Clone Repo to your local machine.
2. In Visual Studio Developer Command Prompt, lanuch solution directly: <root>\Rudderstack.sln.

### Add dependency package
1. Please run below command to restore the packages:
dotnet restore
2. If encounctered 401 error when restoring package, try to see if this command works:
dotnet restore --interactive <project name>

### Setting up MongoDB
1. To connect to MongoDB, run following command on a command shell with <data_directory_path> as the local directory where you want to store the data: 
mongod --dbpath <data_directory_path>
2. On another command shell instance, connect to default test db by running following command:
mongosh
3. Then run below command to connect to our database 'Rudderstack':
use Rudderstack
4. We will be storing 2 collections in our db - FormTemplates and Sources. You can view the documents by using following commands:
db.FormTemplates.find().pretty()  or  db.Sources.find().pretty()

### Build and Debug
1. In VS2019, build and run the app. 
2. Swagger Page will open as soon as the app starts running, which will give the details for all the APIs.

### Some details for Creating FormTemplate
1. Currently, only 3 field input types are supported:
0 -> Checkbox Input,
3 -> Radio Input,
4 -> Text Input
2. FormTemplate.Type represents SourceType and should be unique for each formTemplate.
3. Max SourceType length allowed is 200.
4. Max number of fields allowed is 1000.

### Some Details for creating Source
1. Source.Type represents source type and must have a value that corresponds to an existing FormTemplate in DB. If no formTemplate with this sourceType exists in DB, create Source call will fail.
2. All field values must be given in string form in UserData field
