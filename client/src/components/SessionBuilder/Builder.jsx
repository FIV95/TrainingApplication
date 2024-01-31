import React, { useState } from 'react';
import { Button, Form, Tab, Tabs, Card, ListGroup } from 'react-bootstrap';

const SessionBuilder = () => {
    //Fancy form stuff
    const [showAddSetForm, SetshowAddSetForm] = useState(false);
    
    // Dummy data
    const [inProgressTrainingSession, setInProgressTrainingSession] = useState({});
    const [currentExercise, setCurrentExercise] = useState({});

    // Exercise form variables
    const [reps, setReps] = useState(0);
    const [weight, setWeight] = useState(0);

    // Hardcoded previous sessions
    const [previousSessions, setPreviousSessions] = useState([
        { id: 1, date: '2024-01-20', exercises: ['Squat', 'Bench Press'] },
        { id: 2, date: '2024-01-17', exercises: ['Deadlift', 'Press'] }
    ]);

    // Hard Coded upcoming Session
    const [upcomingSessions, setUpcomingSessions] = useState([
        {
            id: 1,
            date: "2024-02-10",
            exercises: [
                { name: "Squat", sets: 3, reps: 10, weight: "100Ibs" },
                { name: "Bench Press", sets: 3, reps: 8, weight: "60Ibs" },
            ],
        },
        {
            id: 2,
            date: "2024-02-12",
            exercises: [
                { name: "Deadlift", sets: 4, reps: 6, weight: "120Ibs" },
                { name: "Press", sets: 3, reps: 10, weight: "40Ibs" },
            ],
        },
    ]);

    // Hardcoded exercise options
    const exerciseOptions = ['Squat', 'Bench Press', 'Deadlift', 'Press', 'Power Clean'];

    const handleAddSessionClick = () => {
        // Logic to handle adding a new session
    };

    const handleDateSelection = (date) => {
        // Logic to handle date selection
    };

    const handleExerciseSelection = (event) => {
        // Logic to handle exercise selection
        const selectedExercise = event.target.value;
        setCurrentExercise(selectedExercise);
    };

    const handleRepsChange = (event) => {
        setReps(event.target.value);
    }
    const handleWeightsChange = (event) => {
        setWeight(event.target.value);
    }

    const handleAddSet = () => {
        if (showAddSetForm ==  false)
        {
            SetshowAddSetForm(true);
        }
        else
        {
            return SetshowAddSetForm(false);
        }
    };

    const handleAddSetSubmit = (event) => {
        console.log("Submitted Set!");
    }

    const handleSubmit = () => {
        // Logic to submit the session
    };

    return (
        <>
            <div style={{height: "60px"}} className='bg-secondary mb-5'>
                <img className='d-flex flex-start' style={{height: "50px"}} src='https://cdn.imgbin.com/22/5/12/imgbin-hamburger-button-computer-icons-menu-bar-line-4vAWQ1m6s7Hmt7dM6xA0GRhKG.jpg' alt='Hamburger Button'/>
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
                        {/* New Session Form */}
                        {inProgressTrainingSession && (
                            <Form className='text-start'>
                                <Form.Group className='mt-4'>
                                    <Form.Label>Select Date</Form.Label>
                                    <Form.Control type="date" onChange={handleDateSelection} />
                                </Form.Group>
                                <Form.Group className='mt-4'>
                                    <Form.Label>Select Exercise</Form.Label>
                                    <Form.Select aria-label="Select exercise" onChange={handleExerciseSelection}>
                                        {exerciseOptions.map((exercise, index) => (
                                            <option key={index} value={exercise}>{exercise}</option>
                                        ))}
                                    </Form.Select>
                                </Form.Group>
                                <div className= 'mt-4'>
                                    <Button onClick={handleAddSet}>Add Set</Button>
                                    <hr></hr>
                                    {showAddSetForm && (
                                        <Form>
                                            <Form.Group>
                                                <Form.Label>Reps</Form.Label>
                                                <Form.Control type="number" defaultValue={10} onChange={handleRepsChange} />
                                            </Form.Group>
                                            <Form.Group>
                                                <Form.Label>Weight</Form.Label>
                                                <Form.Control type="number" defaultValue={50} onChange={handleWeightsChange} />
                                            </Form.Group>
                                            <Button className='mt-4' onClick={handleAddSetSubmit}>Submit Set</Button>
                                        </Form>
                                    )} {/* Display sets for the current exercise */}
                                    <hr></hr>
                                    <Button onClick={handleSubmit}>Submit Session</Button>
                                    <Button onClick={handleAddSessionClick}>+ Add Session</Button>
                                </div>
                            </Form>
                        )}
                        <hr></hr>
                        {upcomingSessions.map((session) => (
                            <Card key={session.id} className="my-3">
                                <Card.Header as="h5">Due Date: {session.date}</Card.Header>
                                <ListGroup variant="flush">
                                    {session.exercises.map((exercise, index) => (
                                        <ListGroup.Item key={index}>
                                            <strong>{exercise.name}</strong> - Sets: {exercise.sets}, Reps: {exercise.reps}, Weight: {exercise.weight}
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
};

export default SessionBuilder;
