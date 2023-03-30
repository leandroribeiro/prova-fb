# Mars Rover

## Overview

![Image](./docs/diagram.png)

---

## Technologies
- C#
- .NET 6
- XUnit
- Docker

---

## How to Run?


```shell
# Option 1
sh run.sh ${PWD}/data/Sample.txt

# Option 2
sh run.sh
```

With Docker


```shell
# Copy the data file(txt) to root directory (same dir of run-at-docker.sh)

# Option 1
sh run-at-docker.sh data/Sample.txt

# Option 2
sh run-at-docker.sh
```

---

## How to Run Tests

```shell
sh run-tests.sh
```

With Docker

```shell
sh run-tests-at-docker.sh
```