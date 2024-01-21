# brew install protobuf
# go install google.golang.org/protobuf/cmd/protoc-gen-go@v1.28
# go install google.golang.org/grpc/cmd/protoc-gen-go-grpc@v1.2

mkdir -p pb

cp -r ../protobuff/payments.proto ./pb

protodir=./pb
protoc --go_out=$protodir \
    --go-grpc_out=require_unimplemented_servers=false:$protodir \
    -I $protodir $protodir/payments.proto
