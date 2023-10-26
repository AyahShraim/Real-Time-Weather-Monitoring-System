# Real-time Weather Monitoring and Reporting Service

## Table of Contents

- [Overview](#overview)
- [Features](#features)
  - [Supported Formats](#supported-formats)
  - [Weather Bots](#weather-bots)
- [Configuration](#configuration)
- [Example Interaction](#example-interaction)
- [Flowchart](#flowchart)
- [Additional Notes](#additional-notes)

## Overview

This C# console application simulates a real-time weather monitoring and reporting service. It receives and processes weather data in JSON and XML formats from various weather stations.

## Features

### Supported Formats

- JSON: Example format provided.
- XML: Example format provided.

### Weather Bots

- RainBot: Activated when humidity exceeds a threshold.
- SunBot: Activated when temperature rises above a threshold.
- SnowBot: Activated when temperature drops below a threshold.

## Configuration

- Bots are configured via a JSON file.
- Settings include enable/disable, thresholds, and activation messages.

## Example Interaction

1. User inputs weather data in JSON or XML format.
2. Weather bots respond based on data and configurations.

## Flowchart

![DPExcercise](https://github.com/AyahShraim/airport-ticket-booking-system/assets/73714621/ec1a512a-d1c7-473a-bc08-d4c9aca3780b)


## Additional Notes

- Utilizes Observer and Strategy design patterns.
- Supports adding new bot types and data formats with minimal code changes.
