import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Button, Form, Tab, Tabs, Card, ListGroup, Row, Col } from 'react-bootstrap';
import { format, parseISO } from 'date-fns';

// TODO - Add the ability to remove exercises and sets
// TODO - Add the ability to remove individual sets
// TODO - Add button spacing

// ! Judah I installed npm install date-fns
// After importing :
// impor the following:
// ! import { format } from 'date-fns';


const SessionBuilder = () => {
    //Fancy form stuff
    const [showAddSetForm, SetshowAddSetForm] = useState(false);

    // State variables for coach's ID and client's ID
    const [coachId, setCoachId] = useState(null);
    const [clientId, setClientId] = useState(null);

    const [date, setDate] = useState('');


    // State Variable for showing the date selection
    const [showDateSelection, setShowDateSelection] = useState(false);
    const [dateError, setDateError] = useState(null);

    // State Variable for the selected date
    const [selectedDate, setSelectedDate] = useState(null);

    // state variable for showing the "Add a Set" button
    const [showAddSet, setShowAddSet] = useState(false);

    const [isBuilding, setIsBuilding] = useState(false);

    // state variable for api data
    const [exerciseChoices, setExerciseChoices] = useState([]);


    const handleAddSessionClick = () => {
        setShowDateSelection(true);
    };
    // Dummy data
    const [inProgressTrainingSession, setInProgressTrainingSession] = useState({});
    const [currentExercise, setCurrentExercise] = useState({});

    // Exercise form variables
    const [exercises, setExercises] = useState([]);
    const [sets, setSets] = useState([]);
    const [reps, setReps] = useState(0);
    const [weight, setWeight] = useState(0);

    // Hardcoded previous sessions
    const [previousSessions, setPreviousSessions] = useState([
        { id: 1, date: '2024-01-20', exercises: ['Squat', 'Bench Press'] },
        { id: 2, date: '2024-01-17', exercises: ['Deadlift', 'Press'] }
    ]);

    // Hard Coded upcoming Session
    const [upcomingSessions, setUpcomingSessions] = useState([]);

    // Hardcoded exercise options
    const exerciseOptions = ['Squat', 'Bench Press', 'Deadlift', 'Press', 'Power Clean'];



    const handleDateSelection = (event) => {
        const selectedDate = new Date(event.target.value);
        const currentDate = new Date();
        currentDate.setHours(0, 0, 0, 0);

        if (selectedDate <= currentDate) {
            setDateError('The date must be in the future.');
        } else {
            setDateError(null);
            setSelectedDate(event.target.value);
        }
    }

    const handleExerciseSelection = (event) => {
        // Logic to handle exercise selection
        const selectedExercise = event.target.value;
        setCurrentExercise(selectedExercise);

        setShowAddSet(true);
    };

    const handleRepsChange = (event) => {
        setReps(event.target.value);
    }
    const handleWeightsChange = (event) => {
        setWeight(event.target.value);
    }

    const handleAddSet = () => {
        // Create a new set object
        const newSet = {
            setNumber: sets.length + 1,
            reps: reps,
            weight: weight
        };

        // Add the new set to the sets array
        setSets(prevSets => [...prevSets, newSet]);

        // Reset the reps and weight inputs
        setReps('');
        setWeight('');
    };

    const handleAddSetSubmit = (event) => {
        console.log("Submitted Set!");
    }

    const handleSubmit = (event) => {
        event.preventDefault();

        const newSession = {
            date: selectedDate,
            exercises: exercises.map(exercise => ({
                name: exercise.name,
                sets: exercise.sets.map(set => ({
                    setNumber: set.setNumber,
                    reps: set.reps,
                    weight: set.weight
                }))
            }))
        };

        setUpcomingSessions(prevSessions => [...prevSessions, newSession]);
    };

    const handleAddExercise = () => {
        const newExercise = {
            name: currentExercise,
            sets: sets
        };

        setExercises(prevExercises => [...prevExercises, newExercise]);
        setSets([]);
    };

    useEffect(() => {
        const storedCoachId = sessionStorage.getItem('UserId');
        const storedClientId = sessionStorage.getItem('ClientId');

        // Fetch all exercises from the backend using axios
        axios.get('/api/Exercise')
            .then(response => {
                setExerciseChoices(response.data);
            })
            .catch(error => {
                console.error('Error:', error);
            });

        // Fetch the last week of training sessions for the specific client
        axios.get(`/api/TrainingSession/WeeklySessions/${storedClientId}`)
            .then(response => {
                setTrainingSessions(response.data);
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }, []);

    return (
        <>
            <div style={{ height: "60px" }} className='bg-secondary mb-5'>
                <img className='d-flex flex-start' style={{ height: "50px" }} src='https://cdn.imgbin.com/22/5/12/imgbin-hamburger-button-computer-icons-menu-bar-line-4vAWQ1m6s7Hmt7dM6xA0GRhKG.jpg' alt='Hamburger Button' />
            </div>
            <div className='border p-4'>
                <Tabs defaultActiveKey="upcomingSessions">
                    <Tab eventKey="upcomingSessions" title="Upcoming Sessions">
                        {/* List Upcoming Sessions */}
                        {upcomingSessions.map((session) => (
                            <div key={session.id}>
                                {/* Session Details */}
                            </div>
                        ))}
                        {!isBuilding && <Button onClick={() => { handleAddSessionClick(); setIsBuilding(true); }}>+ Add Session</Button>}
                        {showDateSelection && (
                            <Form className='text-start'>
                                <Form.Group className='mt-4'>
                                    <Form.Label>Session Due Date</Form.Label>
                                    <Form.Control type="date" onChange={handleDateSelection} />
                                    {dateError && <div className="text-danger">{dateError}</div>}
                                </Form.Group>
                                {selectedDate && (
                                    <>
                                        {Array.isArray(exercises) && exercises.map((exercise, index) => (
                                            <div key={index}>
                                                <h2>{exercise.name}</h2>
                                                {exercise.sets.map((set, index) => (
                                                    <p key={index}>Set {set.setNumber}: {set.reps} reps, {set.weight} kg</p>
                                                ))}
                                            </div>
                                        ))}

                                        <Form>
                                            {/* Your dropdown menu and other form elements */}
                                        </Form>
                                        <Form.Group className='mt-4'>
                                            <Form.Label>Exercise(s)</Form.Label>
                                            <Form.Select aria-label="Select exercise" onChange={handleExerciseSelection}>
                                                <option value="">Select an exercise</option>
                                                {exerciseOptions.map((exercise, index) => (
                                                    <option key={index} value={exercise}>{exercise}</option>
                                                ))}
                                            </Form.Select>
                                        </Form.Group>
                                        {showAddSet && (
                                            <div className='mt-4'>
                                                {sets.map((set, index) => (
                                                    <div key={index}>
                                                        <p>Set {set.setNumber}: {set.reps} reps, {set.weight} kg</p>
                                                    </div>
                                                ))}
                                                <Form>
                                                    <Row>
                                                        <Col>
                                                            <Form.Group>
                                                                <Form.Label>Reps</Form.Label>
                                                                <Form.Control type="number" value={reps} onChange={handleRepsChange} />
                                                            </Form.Group>
                                                        </Col>
                                                        <Col>
                                                            <Form.Group>
                                                                <Form.Label>Weight</Form.Label>
                                                                <Form.Control type="number" value={weight} onChange={handleWeightsChange} />
                                                            </Form.Group>
                                                        </Col>
                                                    </Row>
                                                </Form>
                                                <Button onClick={handleAddSet}>Add Set</Button>
                                                <Button onClick={handleAddExercise}>Add Exercise</Button>
                                                <Button onClick={handleSubmit}>Confirm Session</Button>
                                                <hr></hr>
                                            </div>
                                        )}
                                    </>
                                )}
                            </Form>
                        )}
                        <hr></hr>
                        {upcomingSessions.map((session, index) => (
                            <Card key={index} className="my-3">
                                <Card.Header as="h5">Due Date: {format(parseISO(session.date), 'EEEE, MMMM do')}</Card.Header>
                                <ListGroup variant="flush">
                                    {session.exercises.map((exercise, index) => (
                                        <ListGroup.Item key={index}>
                                            <strong>{exercise.name}</strong>
                                            {exercise.sets.map((set, index) => (
                                                <p key={index}>Set {set.setNumber}: {set.reps} reps, {set.weight} kg</p>
                                            ))}
                                        </ListGroup.Item>
                                    ))}
                                </ListGroup>
                            </Card>
                        ))}
                    </Tab>
                    <Tab eventKey="previousSessions" title="Previous Sessions">
                        {/* List Previous Sessions */}
                        {previousSessions.map((session) => (
                            <div className='d-flex justify-content-between w-75'>
                                <div className='mt-4 border rounded w-75' key={session.id}>
                                    <p>Date: {session.date}</p>
                                    <p>Exercises: {session.exercises.join(', ')}</p>
                                </div>
                                <button className='m-5 btn bg-secondary text-white'>View Session info</button>
                            </div>
                        ))}
                    </Tab>
                </Tabs>
            </div>
        </>
    );
}
export default SessionBuilder;
