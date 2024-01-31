The session builder form(s) will be the most difficult and techincally demanding feature of our application.

Below outlines the data pieces and the dataflow of those pieces as we navigate to and through the session builder.

1. Upon succesful login (of a coach) their UserId will be stored into session

   1. This id will be 1-of-2 pieces of data that will allow us to properly navigate to our session builder.
2. On the coach dashboard we have our table of **Client Names** each client entry will be tied to their id

   1. This id will be 2-of-2 pieces of data that will allow us to properly navigate to our session builder. We will also store this ClientId into session
   2. Each row in the client table will have an action button to "Add Training Sessions"
   3. Upon clicking that action button we will be redircted to our Training Session Builder Component with both the Coach's ID (sender) & the Client's ID (reciever)
3. ?? To have the Session Builder Process more intuitive and easier for the coach to create sessions we could run one optional query as this Session Builder Component renders

   1. This request will hit a route I developed in out in our back end directory that grabs the last week of training for a given Client/UserId. The response includes all the trainingSessions from the last week.
      1. What this data would allow the Coach/User to do is see a very rough idea of many sessions they have been programming a client per week and what exercises they have done on those days.
      2. All of this information of past sessions would placed in a non-visable tab marked as "Previous Sessions" that the coach could Toggle/View
      3. Previous Sessions are Defined by any TrainingSession - Marked Complete | | Late
         1. "Previous Sessions" would be displaying MOST Recent First
      4. The Alternative/Default Option of the two tabs would be "Upcoming Sessions"
      5. Upcoming Sessions are defined by any TrainingSession - Not Marked Complete && Not Late
         1. "Upcoming Sessions" would be displaying Closest Date To .Now()
4. The second more primary query of the two would be fetching all of the exercises entered into our database

   1. The coach needs this so that they have options when adding TrainingSessionExerises to a TrainingSession
5. Below marks the steps for creating a TrainingSession

   1. The Coach will hit a "+" button titled "Add Session"
      1. this on-click event will render a new component to the screen
      2. This new component will house all the details and forms of our new component
      3. If other "Upcoming-Sessions" are rendered. this new session will remain rendered at the top of the list.
         1. It's very important to Understand this new session box - DOES not mean a new trainingSession has been added to the database or any data has gone back to the server
      4. The Only addional requirement for a TrainingSession to exist is a Date in the future. We already possess the two other requirements CoachId && ClientId.
         1. ?? I don't want the Coach to have to submit this TrainingSession just yet but it may have to be the case.
         2. It may be possible for us to build a que of submissions This Date for TrainingSession one being the first one
         3. Here comes our good friend state
         4. Upon selecting a date (we did not validate it yet because the form for the trainingSession was not submitted yet)
            1. we can create a state variable - InProgressTrainingSession
            2. this object will have the properties of a trainingSession based off our Model
               1. CoachId: "`<ID STORED IN SESSION>`"
               2. ClientId: "`<ID STORED IN SESSION>`"
               3. A Date value
                  1. So by creating this object we have to imagine what this object creation is going to give us back when we send it to the server.
                  2. It'll give us TrainingSession Id of the object we just made
                  3. With  access to this TrainingSession Id we can now make TrainingSessionExercises
6. TrainingSessionExercises require the following properties

   1. TrainingSessionId
   2. ExerciseId
      1. There is one piece of abstract information we need from the server to make this creation possible. We will get to that in step 3.
      2. The form itself will be only one drop down select menu
