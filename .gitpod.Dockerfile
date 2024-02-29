FROM gitpod/workspace-full

# Install Golang
USER root
ENV GO_VERSION=1.22.0

# For ref, see: https://github.com/gitpod-io/workspace-images/blob/61df77aad71689504112e1087bb7e26d45a43d10/chunks/lang-go/Dockerfile#L10
ENV GOPATH=$HOME/go-packages
ENV GOROOT=$HOME/go
ENV PATH=$GOROOT/bin:$GOPATH/bin:$PATH
RUN curl -fsSL https://dl.google.com/go/go${GO_VERSION}.linux-amd64.tar.gz | tar xzs \
    && printf '%s\n' 'export GOPATH=/workspace/go' \
    'export PATH=$GOPATH/bin:$PATH' > $HOME/.bashrc.d/300-go

#Installing Node 20
RUN bash -c 'VERSION="20" \
    && source $HOME/.nvm/nvm.sh && nvm install $VERSION \
    && nvm use $VERSION && nvm alias default $VERSION'

RUN echo "nvm use default &>/dev/null" >> ~/.bashrc.d/51-nvm-fix

# Install .NET
# RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
# RUN dpkg -i packages-microsoft-prod.deb
# RUN apt-get update
# RUN apt-get install -y apt-transport-https
# RUN apt-get update
# RUN apt-get install -y dotnet-sdk-3.1

# Installing Docker-compose
RUN sudo apt-get update && \
    sudo apt-get install -y docker.io && \
    sudo apt-get install -y docker-compose

# Installing GraalVm
RUN bash -c ". /home/gitpod/.sdkman/bin/sdkman-init.sh && \
    sdk install java 22.3.r19-grl && \
    sdk default java 22.3.r19-grl && gu install native-image"

# Switch back to the default user
USER gitpod
