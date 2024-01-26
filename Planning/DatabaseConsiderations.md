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
* Related Entity ID (Can be any entity to which the comment is related, possibly using a polymorphic association)
* CreatedAt
* UpdatedAt

### WeeklySummaries Table

* Summary ID (Primary Key)
* Client ID (Foreign Key from Clients Table)
* Coach ID (Foreign Key from Coaches Table)
* Week Starting Date (to indicate the start of the training week)
* Summary Details (text detailing the training plan for the week)
* CreatedAt
* UpdatedAt

### TrainingSessionExercises Table (Updated)

* Training Session Exercise ID (Primary Key)
* Training Session ID (Foreign Key from TrainingSessions Table)
* Exercise ID (Foreign Key from Exercises Table)
* Prescribed Repetitions
* Prescribed Sets
* Prescribed Weight/Duration/Intensity
* Actual Repetitions (reported by the user)
* Actual Sets (reported by the user)
* Actual Weight/Duration/Intensity (reported by the user)
* Comments/Notes
* CreatedAt
* UpdatedAt

### PerformanceRecords Table (Updated)

* Record ID (Primary Key)
* Client ID (Foreign Key from Clients Table)
* Exercise ID (Foreign Key from Exercises Table)
* Tonnage (total volume lifted, calculated as weight * repetitions * sets)
* Most Weight Lifted
* Most Weight Lifted Date
* Comments/Notes
* CreatedAt
* UpdatedAt
