FROM gitpod/workspace-full


# Install Golang
USER root
RUN apt-get update && apt-get install -y golang

# Install .NET
RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb
RUN apt-get update
RUN apt-get install -y apt-transport-https
RUN apt-get update
RUN apt-get install -y dotnet-sdk-3.1

# Install Rust
RUN curl https://sh.rustup.rs -sSf | sh -s -- -y

# Installing GraalVm
RUN bash -c ". /home/gitpod/.sdkman/bin/sdkman-init.sh && \
    sdk install java 22.3.r19-grl && \
    sdk default java 22.3.r19-grl && gu install native-image"

# Switch back to the default user
USER gitpod