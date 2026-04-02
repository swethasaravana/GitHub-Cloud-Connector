# GitHub Cloud Connector

## Overview

A backend application built using ASP.NET Core Web API that integrates with GitHub APIs. It allows users to fetch repositories, manage issues, and handle milestones through custom endpoints.

---

## Features

* Authenticate using GitHub Personal Access Token (PAT)
* Fetch repositories
* List and create issues
* List commits
* Create and list milestones

---

## Tech Stack

* ASP.NET Core Web API
* HttpClient
* GitHub REST API

---

## Setup

1. Clone the repository:

```
git clone <your-repo-url>
```
2. Generate GitHub Personal Access Token (PAT):
Go to GitHub → Settings
Developer Settings → Personal Access Tokens
Click Generate New Token
Select scope: repo or public_repo
Generate and copy the token

3. Add your GitHub PAT in `appsettings.json`:

```json
"GitHub": {
  "Token": "YOUR_GITHUB_PAT_HERE"
}
```

---

## Run the Project

* Run the application
* Open Swagger:

```
https://localhost:<port>/swagger
```

---

## API Endpoints

* **GET** `/github/repos?owner={username}` → Fetch repositories
* **GET** `/github/repos/{owner}/{repo}/list-issues` → List issues
* **POST** `/github/repos/{owner}/{repo}/create-issues` → Create issue
* **GET** `/github/repos/{owner}/{repo}/list-commits` → List commits
* **GET** `/github/repos/{owner}/{repo}/list-milestones` → List milestones
* **POST** `/github/repos/{owner}/{repo}/create-milestones` → Create milestone

---

## Milestones (Optional)

Milestones help group issues.
Create a milestone → use its number while creating an issue.

---

## Notes

* PAT must have `repo` or `public_repo` access
* Do not expose your token
