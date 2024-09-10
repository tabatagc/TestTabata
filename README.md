CallCenterAgentManager
======================

Overview
--------

The **CallCenterAgentManager** is a backend solution developed in .NET 6 to manage and process call center events related to agents. The main goal of this solution is to provide a REST API to accept agent events and update their states in a relational database (PostgreSQL) or a non-relational database (MongoDB), depending on the configuration.

Additionally, the project includes a VueJS frontend that allows real-time visualization of agent states.

Features
--------

### Level 1 - .NET 6 REST API

*   REST API to accept call center events.
    
*   A single endpoint that accepts a JSON payload with the following structure:
    

```json
{
    "agentId": "6B29FC40-CA47-1067-B31D-00DD010662DA",
    "agentName": "john smith",
    "timestampUtc": "2023-04-23T18:25:43.511Z",
    "action": "CALL_STARTED",
    "queueIds": [
        "3a8cc33a-3f09-4ce5-9c53-e94585a410c8",
        "3d887de3-8351-4391-b155-e174f472456a"
    ]
}
```


*   **Business rules**:
    
    *   If the activity is START\_DO\_NOT\_DISTURB and the timestamp is between 11AM and 1PM UTC, the agent state will be ON\_LUNCH.
        
    *   If the activity is CALL\_STARTED, the agent state will be ON\_CALL.
        
    *   If the timestamp is more than one hour old, a LateEventException will be thrown, resulting in a BadRequest response.
        
*   **PostgreSQL Database**:
    
    *   Update the agent's state based on the above rules.
        
    *   Synchronize the agent's skills based on the queueIds property.
        

### Level 2 - MongoDB Support

*   Support for storing events in either PostgreSQL or MongoDB, depending on a configuration in appsettings.json.
    
*   Leverage Dependency Injection to switch between PostgreSQL and MongoDB without adding complexity to the code.
    

### Level 3 - VueJS Frontend

*   Frontend developed using VueJS to visualize agent states.
    
*   **Key Features**:
    
    *   Agent table with sortable columns by Timestamp and State.
        
    *   Filters to search for agents by name, state, and time range.
        
    *   Detailed agent view showing full agent details and action history.
        
    *   **Optional Dashboard**: Visual graphs representing the distribution of agent states in real-time.
        

Project Structure
-----------------

*   **Domain**: Business logic and domain models, following DDD principles.
    
*   **Application**: Services and use cases for the system.
    
*   **Infrastructure**: Integration with PostgreSQL and MongoDB databases.
    
*   **API**: Controllers and REST API endpoints.
    
*   **Frontend**: VueJS application for agent data visualization.
    

Project Setup
-------------

1.  bashCopy codegit clone https://github.com/tabatagc/callcenteragentmanager.git
    
2.  Example configuration:

```json 
    { 
        "ConnectionStrings": 
        { 
            "PostgreSql": "Host=localhost;Database=callcenter;Username=user;Password=pass", 
            "MongoDb": "mongodb://localhost:27017/callcenter" 
        },
        "DatabaseType": "PostgreSql" // Can be "PostgreSql" or "MongoDb"
    }
```
    
 Edit the appsettings.json file to set the connection strings for PostgreSQL and MongoDB.
        
3.  **Install dependencies**:
    
    *   bashCopy codecd CallCenterAgentManagerdotnet restore
        
    *   bashCopy codecd CallCenterAgentManager/frontendnpm install
        
4.  **Run the project**:
    
    *   bashCopy codedotnet run
        
    *   bashCopy codenpm run serve
        
`

Technologies Used
-----------------

*   **Backend**:
    
    *   .NET 6
        
    *   Entity Framework Core
        
    *   PostgreSQL
        
    *   MongoDB
        
    *   Dependency Injection
        
*   **Frontend**:
    
    *   VueJS
        
    *   TailwindCSS or Vuetify (optional, choose one)
        

Future Improvements
-------------------

*   **Real-Time Updates**: Implement WebSockets to send real-time agent state updates to the frontend.
    
*   **UI Enhancements**: Implement a dashboard with dynamic graphs for a more intuitive agent state visualization.
    

