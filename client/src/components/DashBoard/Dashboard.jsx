import { useState } from 'react'
// import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import { Button, ListGroup, Container, Row, Col } from 'react-bootstrap';
import axios from 'axios'
import {
    useNavigate
} from "react-router-dom";

function DashBoard() {
    const notifications = [
        { id: 1, name: 'Giacomo', action: 'left a comment on their Thursday Workout' },
        { id: 2, name: 'Giacomo', action: 'Missed their Tuesday Workout' },
        { id: 3, name: 'Giacomo', action: 'left a comment on their Tuesday Workout' },
    ];

    return (
        <>
            <div>
                <img className='d-flex flex-start' style={{height: "50px"}} src='https://th.bing.com/th/id/OIP.Au0_7mpqZMtQeoRL4iFkqAHaHa?rs=1&pid=ImgDetMain' alt='Hamburger Button'/>
            </div>
            <h1 className='mb-5'>Welcome, Coach "Insert Name Here"</h1>
            <div className='d-flex justify-content-center'>
            <table className='table table-striped table-hover border w-25 mt-5'>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Giacomo A guy</td>
                        <td>3</td>
                    </tr>
                    <tr>
                        <td>Marco Botton</td>
                        <td>2</td>
                    </tr>
                    <tr>
                        <td>Mariah Maclachlan</td>
                        <td>1</td>
                    </tr>
                    <tr>
                        <td>Valerie Liberty</td>
                        <td>1</td>
                    </tr>
                </tbody>
            </table>

            <Container className='mt-5 border p-2 w-50'>
                <Row className="justify-content-md-center">
                    <Col md={8}>
                    <ListGroup>
                        {notifications.map((notification) => (
                        <ListGroup.Item key={notification.id} className="d-flex justify-content-between align-items-center">
                            Notification {notification.id}: {notification.name} {notification.action}
                            <Button onClick={() => handleReply()} variant="link">reply</Button>
                        </ListGroup.Item>
                        ))}
                    </ListGroup>
                    <div className="mt-2 d-flex justify-content-start">
                        <Button variant="secondary">View Client's Profile</Button>
                    </div>
                    </Col>
                </Row>
            </Container>
            </div>
        </>
    )
}

export default DashBoard


