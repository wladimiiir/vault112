OPTIONS =  -O2 -I./StlPort -I. -fPIC -Wall

all:
	gcc -c fonline_tla.cpp $(OPTIONS) -D__SERVER -o fonline_tla.o
	gcc -shared -Wl,-soname,fonline_tla.so.1 -o fonline_tla.so fonline_tla.o
	gcc -c fonline_test.cpp $(OPTIONS) -D__SERVER -o fonline_test.o
	gcc -shared -Wl,-soname,fonline_test.so.1 -o fonline_test.so fonline_test.o
	gcc -c fonline_tla.cpp $(OPTIONS) -D__CLIENT -o fonline_tla_client.o
	gcc -shared -Wl,-soname,fonline_tla_client.so.1 -o fonline_tla_client.so fonline_tla_client.o
	gcc -c fonline_test.cpp $(OPTIONS) -D__CLIENT -o fonline_test_client.o
	gcc -shared -Wl,-soname,fonline_test_client.so.1 -o fonline_test_client.so fonline_test_client.o
	rm -f fonline_tla.o fonline_test.o fonline_tla_client.o fonline_test_client.o

clean:
	rm -f fonline_tla.o fonline_test.o fonline_tla_client.o fonline_test_client.o
	rm -f fonline_tla.so fonline_test.so fonline_tla_client.so fonline_test_client.so
