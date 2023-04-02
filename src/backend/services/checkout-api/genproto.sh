
mkdir -p pb

cp -r ../protobuff/*.proto ./pb

./node_modules/.bin/proto-loader-gen-types --longs=String --enums=String --defaults --oneofs --grpcLib=@grpc/grpc-js --outDir=src/gen pb/*.proto