<!-- GETTING STARTED -->

## Getting Started

This is an ASP.NET Core API application for backend operations of Pokemon Search Engine.

### Prerequisites

Before running this project, you need to install following:

- [Docker](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/downloads)
- [Visual Studio Code](https://code.visualstudio.com/download)

### How to Run

1. Clone the repo or Extract from zip file.
   ```sh
   git clone https://github.com/amueed/pokemon-app-backend.git
   ```
2. Open Terminal Window in VS Code 'Terminal > New Terminal' and run this command in terminal.
   ```sh
   bash docker-build.sh
   ```
3. After successful build of docker image of this application, a docker image will be created with name `pokemon-app-backend:local`.

4. This image can be run locally on docker enabled machine. To run this image, Run this command in terminal:

   ```sh
   bash docker-run.sh
   ```

5. Browse Application at http://localhost:5000
