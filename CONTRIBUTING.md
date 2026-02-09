# Contributing to Atlas

## Table of Contents

- [Getting Started](#getting-started)
- [Development Setup](#development-setup)
  - [Prerequisites](#prerequisites)
  - [Install Required Tools](#install-required-tools)
- [Git Hooks Setup](#git-hooks-setup)

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/joaovictorjs/Atlas`
3. Create a feature branch following the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/)
4. Make your changes
5. Submit a pull request

## Development Setup

### Prerequisites

- .NET SDK 10.0 or higher
- Git
- Your preferred IDE (Visual Studio, Rider, VS Code)
- CSharpier (code formatter)

### Install Required Tools

#### CSharpier

CSharpier is required for code formatting. Install it locally:

```bash
dotnet tool restore
```

## Git Hooks Setup

This project uses Git hooks to ensure code quality and consistency. **You must configure the hooks before making your first commit.**

### Quick Setup (Recommended)

Run this command in the project root:

```bash
git config core.hooksPath .githooks
```
