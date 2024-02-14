import http from 'k6/http';
import { check, sleep } from 'k6';

const loginEndpoint = 'http://localhost:5138/';
const username = 'test@test.dk';
const password = 'linkin';
const maxVirtualUsers = 1000; // Adjust as needed
const testDurationInSeconds = 30; // Adjust as needed

export let options = {
    stages: [
        { duration: '10s', target: 10 }, // Ramp-up to 10 virtual users in 10 seconds
        { duration: `${testDurationInSeconds}s`, target: maxVirtualUsers }, // Maintain max virtual users for the specified duration
        { duration: '10s', target: 0 }, // Ramp-down to 0 virtual users in 10 seconds
    ],
};

//export const options = {
//    stages: [
//        { duration: '30s', target: 20 },
//        { duration: '10s', target: 10 },
//        { duration: '20s', target: 0 },
//        { duration: '20s', target: 100 },
//        { duration: '20s', target: 500 },
//        { duration: '20s', target: 1000 },
//        { duration: '20s', target: 10000 },
//    ],
//};

//export default function () {
//    const res = http.get('http://localhost:5138/');
//    sleep(1);
//}

export default function () {
    // Generate unique usernames for each virtual user to avoid conflicts
    const uniqueUsername = `${username}_${__VU}`;

    // Make a POST request to the login endpoint with the unique username and password
    let response = http.post(loginEndpoint, {
        username: username,
        password: password,
    });
    console.log(`VU ${__VU}: Login response status - ${response.status}`);

    // Check if the login was successful (adjust the check as per your application's response)
    check(response, {
        'Login successful': (res) => res.status === 200 && res.json('$.success') === true,
    });

    // Simulate think time between requests (adjust as needed)
    sleep(1);
}
