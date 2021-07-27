import React, { Component, useState } from 'react';

import { Container, Button, Row, Col, Form } from 'react-bootstrap';
import axios from 'axios';
import { Redirect } from 'react-router-dom';

const Register = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [newUsername, setNewUsername] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [redirectToChat, setRedirectToChat] = useState(false);

  const handleNewUserSubmit = (e) => {
    e.preventDefault();
    postNewUser();
  };

  const handleLogInSubmit = (e) => {
    e.preventDefault();
    logInUser();
  };

  //   const redirectUser = () => {
  //     if (redirectToChat === true) {
  //       return <Redirect to='/' />;
  //     }
  //   };

  const postNewUser = () => {
    axios
      .post('/api/users', {
        username: newUsername,
        password: newPassword
      })
      .then((res) => {
        localStorage.setItem('session_id', res.data.id);
        localStorage.setItem('user_id', res.data.userId);
        console.log('res tho: ', res.data);
      })
      .catch((err) => {
        handleErrorMessage(err);
      });
  };

  const handleErrorMessage = (err) => {
    switch (err.response.data.title) {
      case 'empty username':
        setErrorMessage('Please choose a username.');
        break;
      case 'empty password':
        setErrorMessage('Please choose a password.');
        break;
      case 'redundant username':
        setErrorMessage('The username you chose is already taken.');
        break;
      case 'wrong credentials':
        setErrorMessage('Username or password combination is invalid.');
        break;
      default:
        alert('Something has gone horribly wrong.');
    }
  };

  const logInUser = () => {
    axios
      .post('/api/authorize', {
        username: username,
        password: password
      })
      .then((res) => {
        localStorage.setItem('session_id', res.data.id);
        localStorage.setItem('user_id', res.data.userId);
        setRedirectToChat(true);
      })
      .catch((err) => {
        handleErrorMessage(err);
      });
  };

  const logInErrorText = () => {
    if (errorMessage === '') {
      return true;
    }
  };

  return (
    <div className='main-container'>
      <Container className='top-container' fluid>
        <Row className='h-100'>
          <Col>
            <h1>Chat app</h1>
          </Col>
          <Col>
            <Form className='sign-in-form' onSubmit={handleLogInSubmit}>
              <Form.Row className='sign-in-row'>
                <Col className='col-xs-5 username-col'>
                  <Form.Label className='top-form-label'>Username</Form.Label>
                  <Form.Control
                    type='input'
                    className='input-sign-in'
                    onChange={(e) => setUsername(e.target.value)}
                    name='existingUsername'
                  />
                </Col>
                <Col className='col-xs-5 password-col'>
                  <Form.Label className='top-form-label'>Password</Form.Label>
                  <Form.Control
                    type='input'
                    className='input-sign-in'
                    onChange={(e) => setPassword(e.target.value)}
                    name='existingPassword'
                  />
                </Col>

                <Button className='login-btn' variant='dark' type='submit'>
                  <h6 className='text-center login-text'>Log In</h6>
                </Button>
              </Form.Row>
            </Form>
            <h6
              //   style={
              //     logInErrorText() ? { display: 'none' } : { display: 'block' }
              //   }
              className='log-in-error'
            >
              {console.log(errorMessage)}
            </h6>
          </Col>
        </Row>
      </Container>
      <Container className='bottom-container' fluid>
        <Row className='w-100'>
          <Col className='w-100'>Register</Col>
        </Row>
        <br />
        <Row>
          <Col>
            <Form className='mt-3 sign-up-form' onSubmit={handleNewUserSubmit}>
              <Form.Group controlId='formBasicEmail'>
                <Form.Label>Choose username</Form.Label>
                <Form.Control
                  type='input'
                  placeholder='Enter a new username'
                  name='newUsername'
                  onChange={(e) => setNewUsername(e.target.value)}
                />
              </Form.Group>

              <Form.Group controlId='formBasicPassword'>
                <Form.Label>Choose password</Form.Label>
                <Form.Control
                  type='password'
                  placeholder='Enter a new password'
                  name='newPassword'
                  onChange={(e) => setNewPassword(e.target.value)}
                />
              </Form.Group>
              <Button variant='light' type='submit'>
                Submit
              </Button>
            </Form>
            <br />
            <h2 className='text-center error-text'>{errorMessage}</h2>
          </Col>
        </Row>
        <div className='container-fluid footer-container'>
          <footer>
            <h6 className='text-center footer-text mt-2'></h6>
          </footer>
        </div>
      </Container>
    </div>
  );
};

export default Register;
