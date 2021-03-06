import React, { useState } from 'react';
import { Form, Button, Col, Row, Container } from 'react-bootstrap';
import axios from 'axios';
import { useHistory } from 'react-router-dom';

const SignUp = () => {
  const [validated, setValidated] = useState(false);
  const [isMatch, setIsMatch] = useState(true);
  const [userName, setUserName] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confPassword, setConfPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState(null);
  const [redirect, setRedirect] = useState(false);

  const handleUsernName = (e) => setUserName(e.target.value);
  const handleFirstName = (e) => setFirstName(e.target.value);
  const handleLastName = (e) => setLastName(e.target.value);
  const handleEmail = (e) => setEmail(e.target.value);
  const handlePassword = (e) => setPassword(e.target.value);
  const handleConfPassword = (e) => {
    setConfPassword(e.target.value);
    setIsMatch(e.target.value === password);
  };
  const history = useHistory();
  const handleSubmit = (e) => {
    e.preDefault();
    const form = e.currentTarget;

    if (form.checkValidity() === false) {
      e.preDefault();
      e.stopPropagation();

      setValidated(true);
      return;
    }

    if (password !== confPassword) {
      e.stopPropagation();
      setIsMatch(false);
      return;
    }

    const userData = {
      username: userName,
      firstName: firstName,
      lastName: lastName,
      email: email,
      password: password
    };

    axios.post('/api/register', userData).then((result) => {
      if (result.data.error) {
        e.stopPropagation();
        setErrorMessage(result.data.error);
      } else {
        //
        console.log(result.data);
        history.push('/');
      }
    });
  };

  if (redirect) {
    history.push('/login');
  }

  return (
    <div id='main' className='d-flex align-items-center h-100'>
      <Container className='bg-light w-75 pb-5 mt-lg-5 mt-2 rounded'>
        <Row className='mt-lg-5 mt-3'>
          <h1 className='text-center'>Create a New Account</h1>
        </Row>
        {errorMessage ? (
          <Row>
            <h6 className='text-center mt-3 mb-0 text-danger'>
              {errorMessage}
            </h6>
          </Row>
        ) : (
          ''
        )}
        <Row lg={2}>
          <Col lg={{ offset: 3 }} className='text-left'>
            <Form noValidate validated={validated} onSubmit={handleSubmit}>
              <Form.Group as={Row} className='mt-lg-5'>
                <Form.Label
                  column
                  lg={4}
                  className='d-flex justify-content-left'
                >
                  User Name:
                </Form.Label>
                <Col>
                  <Form.Control
                    required
                    type='text'
                    onChange={handleUsernName}
                  />
                  <Form.Control.Feedback type='invalid'>
                    First Name is required.
                  </Form.Control.Feedback>
                </Col>
              </Form.Group>
              <Form.Group as={Row} className='mt-lg-5'>
                <Form.Label
                  column
                  lg={4}
                  className='d-flex justify-content-left'
                >
                  First Name:
                </Form.Label>
                <Col>
                  <Form.Control
                    required
                    type='text'
                    onChange={handleFirstName}
                  />
                  <Form.Control.Feedback type='invalid'>
                    First Name is required.
                  </Form.Control.Feedback>
                </Col>
              </Form.Group>
              <Form.Group as={Row} className='mt-2'>
                <Form.Label
                  column
                  lg={4}
                  className='d-flex justify-content-left'
                >
                  Last Name:
                </Form.Label>
                <Col>
                  <Form.Control
                    required
                    type='text'
                    onChange={handleLastName}
                  />
                  <Form.Control.Feedback type='invalid'>
                    Last Name is required.
                  </Form.Control.Feedback>
                </Col>
              </Form.Group>
              <Form.Group as={Row} className='mt-2'>
                <Form.Label
                  column
                  lg={4}
                  className='d-flex justify-content-left'
                >
                  Email:
                </Form.Label>
                <Col>
                  <Form.Control required type='email' onChange={handleEmail} />
                  <Form.Control.Feedback type='invalid'>
                    Email is required.
                  </Form.Control.Feedback>
                </Col>
              </Form.Group>
              <Form.Group as={Row} className='mt-2'>
                <Form.Label
                  column
                  lg={4}
                  className='d-flex justify-content-left'
                >
                  Password:
                </Form.Label>
                <Col>
                  <Form.Control
                    required
                    type='password'
                    onChange={handlePassword}
                  />
                  <Form.Control.Feedback type='invalid'>
                    Password is required.
                  </Form.Control.Feedback>
                </Col>
              </Form.Group>
              <Form.Group as={Row} className='mt-2'>
                <Form.Label
                  column
                  lg={4}
                  className='d-flex justify-content-left'
                >
                  Confirm Password:
                </Form.Label>
                <Col>
                  <Form.Control
                    required
                    type='password'
                    onChange={handleConfPassword}
                    className={isMatch ? '' : 'is-invalid'}
                  />
                  <Form.Control.Feedback type='invalid'>
                    Passwords do not match
                  </Form.Control.Feedback>
                </Col>
              </Form.Group>
              <Form.Group as={Row}>
                <Col className='d-flex justify-content-center'>
                  <Button
                    variant='danger'
                    type='submit'
                    className='mt-lg-5 mt-4 w-50'
                  >
                    Sign Up
                  </Button>
                </Col>
              </Form.Group>
            </Form>
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default SignUp;
