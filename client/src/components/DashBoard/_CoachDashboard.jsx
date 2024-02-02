import { useState } from 'react'
// import 'bootstrap/dist/css/bootstrap.min.css';
import { RxHamburgerMenu } from "react-icons/rx";
import React from 'react';
import { Button, ListGroup, Container, Row, Form, Col } from 'react-bootstrap';
import axios from 'axios'
import {
    useNavigate
} from "react-router-dom";

function CoachDashboard() {
    const navigate = useNavigate();

    const notifications = [
        { id: 1, name: 'Giacomo', action: 'left a comment on their Thursday Workout' },
        { id: 2, name: 'Giacomo', action: 'Missed their Tuesday Workout' },
        { id: 3, name: 'Giacomo', action: 'left a comment on their Tuesday Workout' },
        { id: 4, name: 'Giacomo', action: 'Is not sure how lifting weights works' },
    ];

    const clients = [
        { img: "https://i.redd.it/a9sjrkvtqyf61.jpg", name: "Giacomo A guy", actions: "3" },
        { img: "https://th.bing.com/th/id/OIP.Lcp9INyEq_6OLFmydmne7gAAAA?rs=1&pid=ImgDetMain", name: "Marco Botton", actions: "2" },
        { img: "https://external-preview.redd.it/nDAJx9lUGG-uFz5ZsrLeJwiRVBWNsQecNQgd8z0vLJY.jpg?auto=webp&s=839dff462f20a9a251ac8a5156a50e408f148f8d", name: "Mariah Maclachlan", actions: "1" },
        { img: "https://th.bing.com/th/id/OIP.BraYRCiBc5uASdncgYA7qQHaHa?rs=1&pid=ImgDetMain", name: "Valerie Liberty", actions: "1" },
        { img: "https://th.bing.com/th/id/R.3925851c0e48b256b6b33b7b85bea046?rik=xJCqDIATjiwRRw&riu=http%3a%2f%2fp2.music.126.net%2fAWd90QQDvO3o5QDKKWX_Sw%3d%3d%2f109951164386087590.jpg&ehk=bPf1HLoz0C0m17uL4vKbbVJ3cjwyLbmhd2FyaCXmTtA%3d&risl=&pid=ImgRaw&r=0", name: "A Client", actions: "1" }
    ]


    return (
        <>
            <div className='d-flex justify-content-between header'>
                <RxHamburgerMenu className='hamburger-menu mt-3 ms-3' />
                <div className='d-flex'>
                    <button className='btn btn-danger h-50 mt-3' onClick={e => navigate("/")}>Logout</button>
                    <div style={{borderLeft: "white", marginLeft: "30px"}}>
                        <h1 className='me-3 mt-2'>That Training App</h1>
                    </div>
                </div>
            </div>

            <div className='pt-5 body'>

                <div className='mb-3'>
                    <a href='/dashboard' className='me-5'>Home</a>
                    <a href='/builder' className='me-5'>Session Builder</a>
                    <a href='/construction' className='me-5'>(Under construction) Clients</a>
                    <a href='/construction' className='me-5'>(Under construction) Messages</a>
                </div>

                <h1>Welcome, Coach "Insert Name Here"</h1>

                <div className='d-flex justify-content-center'>

                    <div className='w-25'>
                        <div className='coach-dashboard rounded mt-5 p-3 me-5'>
                            <h3 style={{ textAlign: "start", color: "white" }}>Your Clients</h3>
                            <div style={{ overflowY: "scroll", height: "230px" }}>
                                {clients.map((client, index) => (
                                    <div key={index} className='client m-1 p-4 d-flex rounded justify-content-between'>
                                        <img className='rounded' style={{ height: "75px", width: "75px" }} src={client.img} alt='profile image' />
                                        <div className='d-flex justify-content-between w-75 align-items-center'>
                                            <p>{client.name}</p>
                                            <div className='ms-5'>
                                                <p>Actions</p>
                                                <button style={{borderRadius: "20px"}} className='btn client-button'>{client.actions}</button>
                                            </div>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </div>


                        <div className='add-client mt-3 me-5 rounded p-4'>
                            <Row className="justify-content-md-center">
                                {/* <Col md={8}> */}
                                <Form className='p-3 bg-white border rounded'>
                                    <Form.Group controlId="formBasicEmail">
                                        <h3 className='text-start mb-3'>Add a Client</h3>
                                        <Form.Control type="email" placeholder="Enter email" className='mb-3'/>
                                        <Form.Text className="text-muted">
                                            Your Pupil will grow strong - He will.
                                        </Form.Text>
                                    </Form.Group>
                                    <Button variant="secondary" type="submit" className='mt-3'>
                                        Invite Client
                                    </Button>
                                </Form>
                                {/* </Col> */}
                            </Row>
                        </div>
                    </div>

                    <div className='coach-notif mt-5 p-3 rounded'>
                        <Container>
                            <Row className="justify-content-center">
                                <h1 style={{ color: "white" }}>Notifications</h1>
                                <ListGroup style={{ overflowY: "scroll", height: "250px" }}>
                                    {notifications.map((notification) => (
                                        <div key={notification.id} className="coach-notification d-flex justify-content-between align-items-center p-4 mb-2 rounded">
                                            Notification {notification.id}: {notification.name} {notification.action}
                                            <Button onClick={() => handleReply()} variant="link">reply</Button>
                                        </div>
                                    ))}
                                </ListGroup>
                                <div className="mt-2 d-flex justify-content-start">
                                    <Button variant="secondary">View Client's Sessions</Button>
                                </div>
                            </Row>
                        </Container>
                    </div>
                </div>
            </div>
        </>
    )
}

export default CoachDashboard


