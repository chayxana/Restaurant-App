go install go.k6.io/xk6/cmd/xk6@latest

xk6 build --with github.com/szkiba/xk6-faker@latest

./k6 -q run checkout.js
