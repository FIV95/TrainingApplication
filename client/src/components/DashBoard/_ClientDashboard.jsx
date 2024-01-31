import React, { useState } from 'react';
import { Button, ListGroup, Container, Row, Col, Tabs, Tab, Card } from 'react-bootstrap';

const ClientDashboard = () => {
    const notifications = [
        { id: 1, coachName: 'Coach Carter', comment: 'Great effort on the Thursday Workout!' },
        { id: 2, coachName: 'Coach Carter', comment: 'Remember to complete your Tuesday Workout.' },
        { id: 3, coachName: 'Coach Carter', comment: "Let's try to improve your squat technique next session." },
    ];

    const [previousSessions, setPreviousSessions] = useState([
        { id: 1, date: "2024-01-20", exercises: ["Squat", "Bench Press"] },
        { id: 2, date: "2024-01-17", exercises: ["Deadlift", "Press"] },
    ]);

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

    const handleReply = (notificationId) => {
        // You can add logic here to handle the reply action, like opening a modal or form
        console.log(`Reply to comment with id: ${notificationId}`);
    };

    return (
        <>
            <div style={{ height: "60px" }} className='bg-secondary mb-5'>
                <img className='d-flex flex-start' style={{ height: "50px" }} src='https://cdn.imgbin.com/22/5/12/imgbin-hamburger-button-computer-icons-menu-bar-line-4vAWQ1m6s7Hmt7dM6xA0GRhKG.jpg' alt='Hamburger Button' />
            </div>
            <h1 className='mb-5'>Welcome, Giacomo</h1>
            <div className='d-flex justify-content-center'>
                <div>
                    <Tabs defaultActiveKey="upcomingSessions">
                        <Tab eventKey="upcomingSessions" title="Upcoming Sessions">
                            {/* Render upcoming sessions using Cards */}
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
                                <div key={session.id}>
                                    <p>Date: {session.date}</p>
                                    <p>Exercises: {session.exercises.join(", ")}</p>
                                </div>
                            ))}
                        </Tab>
                    </Tabs>
                </div>
                <Container>
                    <Row className="justify-content-md-center">
                        <Col md={8}>
                            <ListGroup>
                                {notifications.map((notification) => (
                                    <ListGroup.Item key={notification.id} className="d-flex justify-content-between align-items-center">
                                        {notification.coachName} says, "{notification.comment}"
                                        <Button onClick={() => handleReply(notification.id)} variant="link">Reply</Button>
                                    </ListGroup.Item>
                                ))}
                            </ListGroup>
                        </Col>
                    </Row>
                </Container>
            </div>
        </>
    );
};
export default ClientDashboard;
