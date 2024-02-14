from locust import HttpUser, task
from locust.user.task import TaskSetMeta
import random

class TestServer(HttpUser):
    @task
    def get_post(self):
        self.client.get("/api/User?UserId=" + "b51ce5fe-e2cb-46d1-96f6-33d49b9ef5b0") 
        
    @task
    def get_users(self):
        self.client.get("/api/Users/")  