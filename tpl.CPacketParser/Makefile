# -- C compiler --
CC = gcc
CFLAGS = -Ofast -std=c11 -ggdb -Wall -Wno-format -Wno-parentheses

# -- Output library name --
TARGET = packet_parser.dll

default : $(TARGET)

%.dll : %.c
	$(CC) $(CFLAGS) -shared $< -o $@

clean:
	del $(TARGET)