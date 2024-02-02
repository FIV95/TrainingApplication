import React from 'react'
import { useParams, useNavigate, useLocation } from 'react-router-dom'
import axios from 'axios';
import { Button, Form, Tab, Tabs, Card, ListGroup, Row, Col } from 'react-bootstrap';
import { FaChevronDown, FaChevronUp, FaChevronLeft } from 'react-icons/fa';
import { format, parseISO } from 'date-fns';
import { RxHamburgerMenu } from "react-icons/rx";

function SessionView() {
  const location = useLocation();
  const session = location.state.session;
  const navigate = useNavigate();

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

      <div className='body'>

        <div className="container pt-5">

          <div className='mb-3'>
            <a href='/dashboard' className='me-5'>Home</a>
            <a href='/builder' className='me-5'>Session Builder</a>
            <a href='/construction' className='me-5'>(Under construction) Clients</a>
            <a href='/construction' className='me-5'>(Under construction) Messages</a>
          </div>


          <div style={{ textAlign: 'left' }}>
            <FaChevronLeft size={30} onClick={() => navigate(-1)} style={{ cursor: 'pointer' }} />
          </div>
          {/* ... */}
        </div>
        
        <div className="container mt-5 bg-white p-4" style={{ color: 'rgba(119, 141, 169)', borderRadius: '5px' }}>
          <h1 style={{ color: 'rgba(65, 90, 119)' }}>Session View</h1>
          {session && (
            <Card style={{ backgroundColor: 'rgba(13, 27, 42)', color: 'white' }}>
              <Card.Header style={{ backgroundColor: 'rgba(27, 38, 59)', color: 'rgba(119, 141, 169)' }}>
                <h4>Date Issued: {session.date}</h4>
                <h4>Date Completed: {session.completedDate}</h4>
              </Card.Header>
              {session.exercises.map((exercise, index) => (
                <ListGroup variant="flush" key={index}>
                  <ListGroup.Item style={{ backgroundColor: 'rgba(27, 38, 59)', color: 'rgba(119, 141, 169)' }}>
                    <h5>Exercise: {exercise.name}</h5>
                    {exercise.sets.map((set, index) => (
                      <p key={index} style={{ color: 'rgba(224, 225, 221)' }}>Set {set.setNumber}: {set.reps} reps, {set.weight}kg</p>
                    ))}
                  </ListGroup.Item>
                </ListGroup>
              ))}
              <Card.Footer style={{ backgroundColor: 'rgba(27, 38, 59)', color: 'rgba(119, 141, 169)' }}>
                <div>
                  <h2>Comments:</h2>
                  <p className='text-white'><span style={{color: 'rgba(119, 141, 169)', textDecoration: "underline"}}>At: 8:03 am</span> "{session.comments}" - Coach Kahler</p>
                </div>
                <Form>
                  <Form.Group controlId="comment">
                    <Form.Label>Leave a comment</Form.Label>
                    <Form.Control as="textarea" rows={3} />
                  </Form.Group>
                  <Button variant="primary" type="submit" style={{ backgroundColor: 'rgba(65, 90, 119)', borderColor: 'rgba(65, 90, 119)' }}>
                    Submit
                  </Button>
                </Form>
              </Card.Footer>
            </Card>
          )}
        </div>
      </div>
    </>

  )
}

export default SessionView;
