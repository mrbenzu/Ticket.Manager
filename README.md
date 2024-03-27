![Alt text](https://cdn.pixabay.com/photo/2017/06/16/07/26/sign-2408065_1280.png)

# Ticket Manager

Microservices-based .NET Application with Clean Architecture, CQRS, DDD, and Event Storming

# 1. Main goals

Welcome to our microservices-based application! This project is designed using industry-proven methodologies and practices such as Clean Architecture, Command Query Responsibility Segregation (CQRS), Domain Driven Design (DDD), and Event Storming. Our goal is to deliver a robust, scalable, and maintainable solution that aligns with modern software development principles.

### Key Features:

- **Clean Architecture:** Our application is structured using the principles of Clean Architecture, ensuring separation of concerns and maintainability at its core.

- **CQRS (Command Query Responsibility Segregation):** We utilize CQRS to separate the command and query responsibilities, enabling scalability, performance optimization, and better domain modeling.

- **Domain Driven Design (DDD):** With DDD, we focus on modeling the application based on the domain and business requirements, fostering a deeper understanding of the problem domain and improving collaboration between technical and domain experts.

- **Event Storming:** Event Storming is employed to visualize and model complex business domains through collaborative workshops, helping us to identify domain events, commands, and aggregates effectively.

### Testing and Quality Assurance:

- **Unit Tests:** We have comprehensive unit tests in place to ensure the correctness of individual business logic use cases and functionalities within each microservice.

- **Integration Tests:** Integration tests are performed to validate the interaction and behavior of multiple microservices working together.

### Design Patterns and Object-Oriented Programming Principles:

- **Design Patterns:** We employ various design patterns such as Factory, Singleton, Observer, and others to solve common design problems and promote code reusability and maintainability.

- **Object-Oriented Programming Principles:** Our codebase adheres to SOLID principles (Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion), facilitating modular design and easier maintenance.

### Goals:

- **Scalability:** We aim to design a system that can seamlessly scale horizontally to handle increasing loads and user demands.

- **Reliability:** Our focus is on building a reliable system that ensures data consistency, fault tolerance, and high availability.

- **Maintainability:** By adhering to Clean Architecture and employing best practices, we strive to create a codebase that is easy to understand, extend, and maintain over time.

- **Performance:** We prioritize performance optimization at every level of the application to deliver fast response times and optimal resource utilization.

- **Flexibility:** Our architecture allows for flexibility in adapting to changing business requirements and integrating new features seamlessly.


# 2. Requirements for Online Ticket Selling System

## User Account Management Functionality
- Create a user account.
- Login and logout.
- Manage personal data (edit, delete, view).
- Purchase history.

## Event Search and Browsing Functionality
- Ability to browse events by category, date, location, etc.
- Detailed event information (description, date, time, location, ticket prices, availability).

## Ticket Purchase Functionality
- Add tickets to the cart.
- Select ticket types (e.g., standard, VIP, group).
- Purchase confirmation.

## Ticket Reservation Functionality
- Maximum reservation time is 15 minutes
- Adjacent seats cannot be left empty if one is already reserved

## Notification System
- Notifications about new events in the user's preferred categories or locations.
- Notifications regarding ticket purchase confirmation, event changes, etc.

## Discounts and Promotions Management
- Ability to enter discount codes.
- Automatic application of promotions in the cart.

## Event and Ticket Management
- Administrative panel for adding, editing, and deleting events and tickets.
- Managing ticket availability.
- Generating sales reports and statistics.

## Support for Multiple Locations
- Ability to add events taking place at different locations.
- Setting unique parameters for each location (e.g., venue capacity, seating maps).

# 3. Event Storming - Process Level

# Event

## Create event

![image](https://github.com/mrbenzu/Ticket.Manager/assets/42185582/d17b9416-416f-440c-9bfa-e13923439e60)

## Suspend event

![image](https://github.com/mrbenzu/Ticket.Manager/assets/42185582/e1b18d4c-3045-43a4-8b8b-fdd4f640c0a8)

## Reopen event

![image](https://github.com/mrbenzu/Ticket.Manager/assets/42185582/7d598884-88c7-4d03-bdd2-7de117fd1603)

## Cancel event

![image](https://github.com/mrbenzu/Ticket.Manager/assets/42185582/a5a3aa55-2ea1-4fd4-9ebb-5eb677781825)

# Seat

## Reserve seat

![image](https://github.com/mrbenzu/Ticket.Manager/assets/42185582/6a0e41b4-1ad2-462f-8068-f0548c6864f2)

## Withdraw seat

![image](https://github.com/mrbenzu/Ticket.Manager/assets/42185582/c1841218-ba93-484b-9491-41c580e7782a)

## Clear basket

![image](https://github.com/mrbenzu/Ticket.Manager/assets/42185582/37f62419-d578-45e1-b527-86b1ad041998)

# Order

## Create order

![image](https://github.com/mrbenzu/Ticket.Manager/assets/42185582/9ed2eb3d-c6b2-4d20-ab52-6783318ac29a)

# Ticket

## Return ticket

![image](https://github.com/mrbenzu/Ticket.Manager/assets/42185582/6627d662-676e-49a1-99f0-02bbfd3818a7)

# 4. Roadmap

List of tasks

| Nr. | Task              | Completed | Completion Date |
|-----|-------------------|-----------|-----------------|
| 1   | Add Events module |        |      |
| 2   | Add Seats module  |       |                 |
| 3   | Add Order module  |       |                 |
| 4   | ....  |       |                 |
