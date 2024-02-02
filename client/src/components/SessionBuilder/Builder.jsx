import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Button, Form, Tab, Tabs, Card, ListGroup, Row, Col } from 'react-bootstrap';
import { FaChevronDown, FaChevronUp } from 'react-icons/fa';
import { format, parseISO } from 'date-fns';
import { RxHamburgerMenu } from "react-icons/rx";

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
    const navigate = useNavigate();



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
        {
            id: 1,
            date: "2024-01-20",
            completedDate: "2024-01-21",
            comments: "Great effort!",
            exercises: [
                {
                    name: "Squat",
                    sets: [
                        { setNumber: 1, reps: 10, weight: "150" },
                        { setNumber: 2, reps: 10, weight: "150" },
                        { setNumber: 3, reps: 10, weight: "170" },
                    ],
                },
                {
                    name: "Bench Press",
                    sets: [
                        { setNumber: 1, reps: 10, weight: "100" },
                        { setNumber: 2, reps: 10, weight: "100" },
                        { setNumber: 3, reps: 10, weight: "100" },
                    ],
                },
                {
                    name: "Deadlift",
                    sets: [
                        { setNumber: 1, reps: 5, weight: "150" },
                        { setNumber: 2, reps: 5, weight: "150" },
                        { setNumber: 3, reps: 5, weight: "150" },
                    ],
                },
            ],
        },
        {
            id: 2,
            date: "2024-01-17",
            exercises: [
                {
                    name: "Press",
                    sets: [
                        { setNumber: 1, reps: 10, weight: "60" },
                        { setNumber: 2, reps: 10, weight: "60" },
                        { setNumber: 3, reps: 10, weight: "60" },
                    ],
                },
                {
                    name: "Power Clean",
                    sets: [
                        { setNumber: 1, reps: 5, weight: "100" },
                        { setNumber: 2, reps: 5, weight: "100" },
                        { setNumber: 3, reps: 5, weight: "100" },
                    ],
                },
            ],
        },
    ]);

    // Hard Coded upcoming Session
    const [upcomingSessions, setUpcomingSessions] = useState([]);

    // Hardcoded exercise options
    const exerciseOptions = ['Squat', 'Bench Press', 'Deadlift', 'Press', 'Power Clean'];
    // const [open, setOpen] = useState(Array(exercises.length).fill(false));



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

    const handleViewClick = (session) => {
        navigate(`/session/${session.id}`, { state: { session } });
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

            <div className='d-flex justify-content-between header'>
                <RxHamburgerMenu className='hamburger-menu mt-3 ms-3' />
                <div className='d-flex'>
                    <button className='btn btn-danger h-50 mt-3' onClick={e => navigate("/")}>Logout</button>
                    <div style={{ borderLeft: "white", marginLeft: "30px" }}>
                        <h1 className='me-3 mt-2'>That Training App</h1>
                    </div>
                </div>
            </div>

            <div style={{ paddingLeft: "300px", paddingRight: "300px" }} className='body border pt-5'>
                <div className='mb-3'>
                    <a href='/dashboard' className='me-5'>Home</a>
                    <a href='/builder' className='me-5'>Session Builder</a>
                    <a href='/construction' className='me-5'>(Under construction) Clients</a>
                    <a href='/construction' className='me-5'>(Under construction) Messages</a>
                </div>

                <Tabs defaultActiveKey="upcomingSessions" className='my-tabs'>
                    <Tab className='bg-white' eventKey="upcomingSessions" title="Upcoming Sessions">
                        {/* List Upcoming Sessions */}
                        {upcomingSessions.map((session) => (
                            <div key={session.id}>
                                {/* Session Details */}
                            </div>
                        ))}
                        {!isBuilding && <Button style={{ marginTop: '10px' }} onClick={() => { handleAddSessionClick(); setIsBuilding(true); }}>+ Add Session</Button>}
                        {showDateSelection && (
                            <Form className='text-start p-3'>
                                <Form.Group className='pt-4'>
                                    <Form.Label>Session Due Date</Form.Label>
                                    <Form.Control type="date" onChange={handleDateSelection} />
                                    {dateError && <div className="text-danger">{dateError}</div>}
                                </Form.Group>
                                {selectedDate && (
                                    <>
                                        {Array.isArray(exercises) && exercises.map((exercise, index) => (
                                            <div key={index} className='mt-4'>
                                                <h2>{exercise.name}</h2>
                                                {exercise.sets.map((set, index) => (
                                                    <p key={index}>Set {set.setNumber}: {set.reps} reps, {set.weight} kg</p>
                                                ))}
                                            </div>
                                        ))}

                                        <Form>
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

                                                <div className='ms-3 mt-3'>
                                                    <Button onClick={handleAddSet} className="me-3">Add Set</Button>
                                                    <Button onClick={handleAddExercise} className="me-3">Add Exercise</Button>
                                                    <Button onClick={handleSubmit}>Confirm Session</Button>
                                                </div>
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
                    <Tab className='bg-white' eventKey="previousSessions" title="Previous Sessions">
                        <div style={{
                            display: 'flex',
                            flexWrap: 'wrap',
                            justifyContent: 'flex-start',
                            gap: '20px',
                            backgroundColor: 'rgba(224, 225, 221)', // Light background for the tab content
                            padding: '20px'
                        }}>
                            {previousSessions.map((session, sessionIndex) => (
                                <Card key={sessionIndex} className="my-3 d-flex flex-column" style={{
                                    width: '18rem',
                                    flex: '0 0 auto',
                                    backgroundColor: 'rgba(13, 27, 42)', // Dark card background
                                    color: 'white', // Text color
                                    borderRadius: '5px'
                                }}>
                                    <Card.Header as="h5" style={{
                                        backgroundColor: 'rgba(27, 38, 59)',
                                        color: 'white'
                                    }}>Date: {format(parseISO(session.date), 'EEEE, MMMM do')}</Card.Header>
                                    <ListGroup variant="flush" className="flex-grow-1">
                                        {session.exercises.map((exercise, exerciseIndex) => {
                                            const [open, setOpen] = useState(Array(session.exercises.length).fill(false));
                                            return (
                                                <ListGroup.Item key={exerciseIndex} style={{
                                                    backgroundColor: 'rgba(27, 38, 59)', // Darker background for list items
                                                    color: 'rgba(119, 141, 169)' // Mid-tone text color
                                                }}>
                                                    <strong>{exercise.name}</strong>
                                                    {exercise.sets.length >= 3 && (
                                                        <div
                                                            style={{
                                                                cursor: 'pointer',
                                                                fontSize: '0.75rem',
                                                                color: 'rgba(65, 90, 119)' // Deep blue text color for clickable items
                                                            }}
                                                            onClick={() => {
                                                                const newOpen = [...open];
                                                                newOpen[exerciseIndex] = !newOpen[exerciseIndex];
                                                                setOpen(newOpen);
                                                            }}
                                                        >
                                                            View Set Details {open[exerciseIndex] ? <FaChevronUp /> : <FaChevronDown />}
                                                        </div>
                                                    )}
                                                    {open[exerciseIndex] && exercise.sets.map((set, setIndex) => (
                                                        <p key={setIndex} style={{
                                                            color: 'rgba(224, 225, 221)' // Light text for the details
                                                        }}>Set {set.setNumber}: {set.reps} reps, {set.weight} kg</p>
                                                    ))}
                                                </ListGroup.Item>
                                            );
                                        })}
                                    </ListGroup>
                                    <Card.Footer className="text-muted" style={{
                                        backgroundColor: 'rgba(27, 38, 59)',
                                        color: 'rgba(119, 141, 169)'
                                    }}>
                                        <button
                                            onClick={() => handleViewClick(session)}
                                            style={{
                                                backgroundColor: 'rgba(65, 90, 119)',
                                                color: 'white',
                                                border: 'none',
                                                padding: '5px 10px',
                                                borderRadius: '5px',
                                                cursor: 'pointer'
                                            }}
                                        >
                                            View Session
                                        </button>
                                    </Card.Footer>
                                </Card>
                            ))}
                        </div>
                    </Tab>

                </Tabs>
            </div>
        </>

    );
}
export default SessionBuilder;
