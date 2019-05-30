# NetCoreLearning
Learning .Net Core 2.2

This solution is a dashboard showing live the daily grade assignments by professors to students for specific lessons.

The Ui is Razor Pages with SignalR javascript client.
In the same project there is a SignalR Hub that reads the Grade Assigments of the day from a Redis distributed cache.

How this Redis distributed cache gets filled:

There is a Web APi that uses JWT Authentication. It is connected with an SQL Database. Through the API aa user can G 
