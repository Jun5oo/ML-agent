# ML agent

**주제**: "강화학습을 이용한 게임AI 구현" 

Unity ML agent를 사용해 특정 행동을 스스로 학습하는 AI를 구현 

# Implementation1 

**목표**: 날아드는 장애물(총알)을 피하면서 목표 지점에 도달하기. 

**내용**: Reinforcement-Learning-KR 에서 작성된 ML-agent Dodge 학습을 바탕으로 추가 학습을 진행 

**관측값**: 360도 방향 40개의 raycast를 통해 거리, 오브젝트 속도, 에이전트와 타겟 사이의 x, z축 distance, 에이전트의 x, z축 방향 속도 

**행동값**: 상, 하, 좌, 우 방향으로 agentSpeed 만큼 이동 

**보상값**: 장애물(총알)에 충돌하면 -5점, 목표 타겟에 충돌하면 +10점, 타겟과의 거리가 줄어들면 0.01점, 그렇지 않으면 -0.01점 

**학습 결과1** 

![ML_Agent Project - New Scene - Windows, Mac, Linux - Unity 2023 (1)](https://github.com/user-attachments/assets/a0d16dd7-d65b-4ab4-ae0c-36aa06925cba)

![image](https://github.com/user-attachments/assets/253a3573-7b89-4993-899c-f796b6385412)

현재까지 약 4700만번의 step을 진행하였고, mean reward는 평균 7점 초반이며 아직까지 결과가 크게 만족스럽지 않다. 

아직까지도 학습은 진행 중이며, 평균 9점까지 학습을 시켜볼 예정이다. 


**학습 결과2** 
![Project 2024-10-30 13-51-10](https://github.com/user-attachments/assets/494cbbdc-f89c-440b-9896-389cc9d48d02)

기존 Action은 AddForce로 가속도가 너무 붙어서 속도로 밀어붙이는 느낌이 있어 조금 변경해주었다. 
위 학습에서 이어서 진행했으며, 조금 더 학습이 필요한 것으로 보인다. 

# References

https://github.com/reinforcement-learning-kr/Unity_ML_Agents_2.0
