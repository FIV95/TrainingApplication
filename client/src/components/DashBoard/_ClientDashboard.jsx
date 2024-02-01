import React, { useState, useEffect, Link } from 'react';
import { Button, ListGroup, Container, Row, Col, Tabs, Tab, Card } from 'react-bootstrap';
import { FaChevronDown, FaChevronUp } from 'react-icons/fa';

const ClientDashboard = () => {
    const notifications = [
        { id: 1, coachName: 'Coach Carter', comment: 'Great effort on the Thursday Workout!' },
        { id: 2, coachName: 'Coach Carter', comment: 'Remember to complete your Tuesday Workout.' },
        { id: 3, coachName: 'Coach Carter', comment: "Let's try to improve your squat technique next session." },
    ];

    const [firstName, setFirstName] = useState(null);


    const [previousSessions, setPreviousSessions] = useState([
        {
            id: 1,
            date: "2024-01-20",
            exercises: [
                {
                    name: "Squat",
                    sets: [
                        { setNumber: 1, reps: 10, weight: "150Ibs" },
                        { setNumber: 2, reps: 10, weight: "150Ibs" },
                        { setNumber: 3, reps: 10, weight: "170Ibs" },
                    ],
                },
                {
                    name: "Bench Press",
                    sets: [
                        { setNumber: 1, reps: 10, weight: "100Ibs" },
                        { setNumber: 2, reps: 10, weight: "100Ibs" },
                        { setNumber: 3, reps: 10, weight: "100Ibs" },
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
                        { setNumber: 1, reps: 10, weight: "60Ibs" },
                        { setNumber: 2, reps: 10, weight: "60Ibs" },
                        { setNumber: 3, reps: 10, weight: "60Ibs" },
                    ],
                },
                {
                    name: "Power Clean",
                    sets: [
                        { setNumber: 1, reps: 5, weight: "100Ibs" },
                        { setNumber: 2, reps: 5, weight: "100Ibs" },
                        { setNumber: 3, reps: 5, weight: "100Ibs" },
                    ],
                },
            ],
        },
    ]);

    const [upcomingSessions, setUpcomingSessions] = useState([
        {
            id: 1,
            dueDate: "2024-02-10",
            exercises: [
                {
                    name: "Squat",
                    sets: [
                        { setNumber: 1, reps: 10, weight: "100Ibs" },
                        { setNumber: 2, reps: 10, weight: "100Ibs" },
                        { setNumber: 3, reps: 10, weight: "100Ibs" },
                    ]
                },
                {
                    name: "Bench Press",
                    sets: [
                        { setNumber: 1, reps: 8, weight: "60Ibs" },
                        { setNumber: 2, reps: 8, weight: "60Ibs" },
                        { setNumber: 3, reps: 8, weight: "60Ibs" },
                    ]
                },
            ],
        },
        {
            id: 2,
            dueDate: "2024-02-12",
            exercises: [
                {
                    name: "Deadlift",
                    sets: [
                        { setNumber: 1, reps: 6, weight: "120Ibs" },
                        { setNumber: 2, reps: 6, weight: "120Ibs" },
                        { setNumber: 3, reps: 6, weight: "120Ibs" },
                        { setNumber: 4, reps: 6, weight: "120Ibs" },
                    ]
                },
                {
                    name: "Press",
                    sets: [
                        { setNumber: 1, reps: 10, weight: "40Ibs" },
                        { setNumber: 2, reps: 10, weight: "40Ibs" },
                        { setNumber: 3, reps: 10, weight: "40Ibs" },
                    ]
                },
            ],
        },
    ]);

    const [showSets, setShowSets] = useState(Array(upcomingSessions.length).fill(false));
    const [showPreviousSets, setShowPreviousSets] = useState(Array(previousSessions.length).fill(false));

    const handleReply = (notificationId) => {
        // You can add logic here to handle the reply action, like opening a modal or form
        console.log(`Reply to comment with id: ${notificationId}`);
    };

    useEffect(() => {
        const firstNameFromSession = sessionStorage.getItem('firstName');
        setFirstName(firstNameFromSession);
    })

    return (
        <Container>
            <Row>
                <Col>
                    <h1>Welcome, {firstName}!</h1>
                    <Row>
                        <Col>
                            <Tabs defaultActiveKey="upcomingSessions" id="uncontrolled-tab-example">
                                <Tab eventKey="previousSessions" title="Previous Sessions">
                                    {previousSessions.map((session, sessionIndex) => (
                                        <Card key={session.id} className="mb-3">
                                            <Card.Header>
                                                Date: {session.date}
                                            </Card.Header>
                                            {session.exercises.map((exercise, exerciseIndex) => (
                                                <Card.Body key={exerciseIndex}>
                                                    <Card.Title>{exercise.name}</Card.Title>
                                                    {showSets[sessionIndex] && exercise.sets.map((set, setIndex) => (
                                                        <Card.Text key={setIndex}>
                                                            Set {set.setNumber}: {set.reps} reps, {set.weight}
                                                        </Card.Text>
                                                    ))}
                                                </Card.Body>
                                            ))}
                                            <Card.Footer className="d-flex justify-content-between align-items-center">
                                                <div
                                                    style={{ fontStyle: 'italic', cursor: 'pointer', marginRight: '20px' }}
                                                    onClick={() => {
                                                        const newShowSets = [...showSets];
                                                        newShowSets[sessionIndex] = !newShowSets[sessionIndex];
                                                        setShowSets(newShowSets);
                                                    }}
                                                >
                                                    View Sets
                                                    {showSets[sessionIndex] ? <FaChevronUp /> : <FaChevronDown />}
                                                </div>
                                                <div style={{ borderLeft: '1px solid #000', height: '100%', marginRight: '10px' }}></div>
                                                <a href="#" style={{ marginLeft: '20px' }}>View Session</a>
                                            </Card.Footer>
                                        </Card>
                                    ))}
                                </Tab>
                                <Tab eventKey="upcomingSessions" title="Upcoming Sessions">
                                    {upcomingSessions.map((session, sessionIndex) => (
                                        <Card key={session.id} className="mb-3">
                                            <Card.Header>
                                                Due: {new Date(session.dueDate).toLocaleDateString('en-US', { weekday: 'long', month: 'long', day: 'numeric' })}
                                            </Card.Header>
                                            {session.exercises.map((exercise, exerciseIndex) => (
                                                <Card.Body key={exerciseIndex}>
                                                    <Card.Title>{exercise.name}</Card.Title>
                                                    {showSets[sessionIndex] && exercise.sets.map((set, setIndex) => (
                                                        <Card.Text key={setIndex}>
                                                            Set {set.setNumber}: {set.reps} reps, {set.weight}
                                                        </Card.Text>
                                                    ))}
                                                </Card.Body>
                                            ))}
                                            <Card.Footer className="d-flex justify-content-between align-items-center">
                                                <div
                                                    style={{ fontStyle: 'italic', cursor: 'pointer', marginRight: '20px' }}
                                                    onClick={() => {
                                                        const newShowSets = [...showSets];
                                                        newShowSets[sessionIndex] = !newShowSets[sessionIndex];
                                                        setShowSets(newShowSets);
                                                    }}
                                                >
                                                    View Sets
                                                    {showSets[sessionIndex] ? <FaChevronUp /> : <FaChevronDown />}
                                                </div>
                                                <div style={{ borderLeft: '1px solid #000', height: '100%', marginRight: '10px' }}></div>
                                                <a href="#" style={{ marginLeft: '20px' }}>View Session</a>
                                            </Card.Footer>
                                        </Card>
                                    ))}
                                </Tab>
                            </Tabs>
                        </Col>
                        <Col>
                            <h2>Notifications</h2>
                            <ListGroup>
                                {notifications.map((notification) => (
                                    <ListGroup.Item key={notification.id}>
                                        {notification.coachName}: {notification.comment}
                                        <Button variant="link">Reply</Button>
                                    </ListGroup.Item>
                                ))}
                            </ListGroup>
                        </Col>
                    </Row>
                </Col>
            </Row>
        </Container>
    );
}
export default ClientDashboard;