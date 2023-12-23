# AzureStorage
Cloud development project with Azure Storage: Blob Container, Tables, and Queues

This project was built with:
A simple CRUD to record data in Storage Tables and it will save an avatar image in a Blob Container.
After each action of the CRUD an event create a message in a queue.
The Consumer console application consumes the messages in the queue.


![image](https://github.com/marcelonespoli/AzureStorage/assets/23698989/25e60b03-9c7a-4ae3-aa4e-b8a2409b88b0)


The environment variables can be updated to any Storage Account and here is an example of the Blob Container, Queue, and Table used for this project:

![image](https://github.com/marcelonespoli/AzureStorage/assets/23698989/d98adb51-21e8-407b-8e17-76953245b961)


Developed using C#, .NET 7, ASP.NET Core, Azure Storage, and Console Application running in WSL.
