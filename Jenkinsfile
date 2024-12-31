pipeline {
    agent any

    environment {
        DOCKER_IMAGE = 'khaoulakha/repohub:latest'  // Replace with your Docker Hub repository and image name
        REMOTE_SERVER = 'a@172.29.224.1'  // Replace with your SSH username and the remote server's IP
    }

    stages {
        // Stage for building the application (ASP.NET Core)
        stage('Build') {
            steps {
                script {
                    echo 'Building the ASP.NET Core application...'
                    // Build the ASP.NET Core application using the solution file
                    sh 'dotnet restore ProjetPfa.sln'  // Restore NuGet packages for the correct solution file
                    sh 'dotnet build ProjetPfa.sln --configuration Release'  // Build the solution in Release mode
                }
            }
        }

        // Stage for running unit tests (ASP.NET Core)
        stage('Unit Tests') {
            steps {
                script {
                    echo 'Running unit tests...'
                    // Run tests using the .NET Core test command
                    sh 'dotnet test ProjetPfa.Tests/ProjetPfa.Tests.csproj --configuration Release'  // Run the tests
                }
            }
        }

        // Stage for creating the Docker image
        stage('Build Docker Image') {
            steps {
                script {
                    echo 'Building Docker image...'
                    // Build the Docker image
                    sh 'docker build -t ${DOCKER_IMAGE} .'  // Build the Docker image using the Dockerfile in the current directory
                }
            }
        }

        // Stage for pushing the Docker image to Docker Hub
        stage('Push to Docker Hub') {
            steps {
                script {
                    echo 'Pushing Docker image to Docker Hub...'
                    // Log in to Docker Hub using credentials stored in Jenkins
                    withCredentials([usernamePassword(credentialsId: 'dockerhub-creds', usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')]) {
                        sh 'docker login -u ${DOCKER_USERNAME} -p ${DOCKER_PASSWORD}'  // Log into Docker Hub
                    }
                    // Push the Docker image to Docker Hub
                    sh 'docker push ${DOCKER_IMAGE}'  // Push the image to Docker Hub
                }
            }
        }

        // Stage for deploying the Docker image to the remote server
        stage('Deploy to Remote Server') {
            steps {
                script {
                    echo 'Deploying Docker container to remote server...'
                    // SSH into the remote server and run the Docker container
                    withCredentials([sshUserPrivateKey(credentialsId: 'ssh-remote-server-creds', keyFileVariable: 'SSH_KEY')]) {
                        sh """
                            ssh -i ${SSH_KEY} ${REMOTE_SERVER} '
                                docker pull ${DOCKER_IMAGE} && 
                                docker stop app || true && 
                                docker rm app || true && 
                                docker run -d --name app -p 8080:80 ${DOCKER_IMAGE}
                            '
                        """
                    }
                }
            }
        }

        // Stage for running DevSecOps security scans (using Trivy as an example)
        stage('DevSecOps') {
            steps {
                echo 'Running DevSecOps security scan...'
                // Example: Run a security scan using Trivy for container security
                sh 'trivy image ${DOCKER_IMAGE}'  // Scan the built Docker image for vulnerabilities
            }
        }

        // Stage for SonarQube code quality analysis (ASP.NET Core)
        stage('SonarQube') {
            steps {
                echo 'Running SonarQube analysis...'
                script {
                    // Run SonarQube analysis for .NET Core projects
                    // Replace with SonarScanner for MSBuild or other tools if needed
                    sh '''
                        SonarScanner.MSBuild.exe begin /k:"your-project-key" /d:sonar.login="your-token"
                        dotnet build
                        SonarScanner.MSBuild.exe end /d:sonar.login="your-token"
                    '''  // Run the analysis with SonarScanner for MSBuild
                }
            }
        }
    }
}
