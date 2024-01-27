### Coaches Table

* Coach ID (Primary Key)
* First Name
* Last Name
* Email
* Password (hashed)
* CreatedAt
* UpdatedAt

### Clients Table

* Client ID (Primary Key)
* First Name
* Last Name
* Email
* Password (hashed)
* Coach ID (Foreign Key from Coaches Table)
* CreatedAt
* UpdatedAt

### Exercises Table

* Exercise ID (Primary Key)
* Name
* Description
* Instructional Video Link
* CreatedAt
* UpdatedAt

### TrainingSessions Table

* Training Session ID (Primary Key)
* Coach ID (Foreign Key from Coaches Table)
* Client ID (Foreign Key from Clients Table)
* Due Date
* CreatedAt
* UpdatedAt

### Comments Table

* Comment ID (Primary Key)
* Content
* Author ID (Can be either a Coach ID or Client ID, consider using a polymorphic association)

  *[https://medium.com/@hendelRamzy/what-is-polymorphic-relationships-and-how-they-work-61df8d1562c5]()*
* Related Entity ID (Can be any entity to which the comment is related, possibly using a polymorphic association)
* CreatedAt
* UpdatedAt

### WeeklySummaries Table

* Summary ID (Primary Key)
* Client ID (Foreign Key from Clients Table)
* Coach ID (Foreign Key from Coaches Table)
* Summary Details (text detailing the training plan for the week)
* CreatedAt
* UpdatedAt

### TrainingSessionExercises Table

* Training Session Exercise ID (Primary Key)
* Training Session ID (Foreign Key from TrainingSessions Table)
* Exercise ID (Foreign Key from Exercises Table)
* Prescribed Repetitions
* Prescribed Sets
* Prescribed Weight
* Actual Repetitions (reported by the user) ?
* Actual Sets (reported by the user) ?
* Actual Weight Used (reported by the user) ?
* Results `<textfield>`
* CompletedAsPrescribed `<bool> `
* Comments/Notes (reported by the user)
* CreatedAt
* UpdatedAt

### ~~PerformanceRecords Table~~

* ~~Record ID (Primary Key)~~
* ~~Client ID (Foreign Key from Clients Table)~~
* ~~Exercise ID (Foreign Key from Exercises Table)~~
* ~~Tonnage (total volume lifted, calculated as weight * repetitions * sets)~~
* ~~Most Weight Lifted~~
* ~~Most Weight Lifted Date~~
* ~~Comments/Notes~~
* ~~CreatedAt~~
* ~~UpdatedAt~~

### ~~TrainingSessionExerciseSets Table~~

* ~~Set ID (Primary Key)~~
* ~~Training Session Exercise ID (Foreign Key from TrainingSessionExercises Table)~~
* ~~Set Number (e.g., Set 1, Set 2, etc.)~~
* ~~Repetitions Achieved~~
* ~~Weight Used~~
* ~~Comments/Notes (specific to the set)~~
* ~~CreatedAt~~
* ~~UpdatedAt~~
