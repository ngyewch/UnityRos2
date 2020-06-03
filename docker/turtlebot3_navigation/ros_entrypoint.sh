#!/bin/bash
set -e
# setup ros environment
source "/opt/ros/dashing/setup.bash"
source "$DEPENDENCIES_WS/install/local_setup.bash"
source "$APP_WS/install/local_setup.bash"
exec "$@"
