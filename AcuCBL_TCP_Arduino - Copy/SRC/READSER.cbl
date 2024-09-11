       IDENTIFICATION DIVISION.
       PROGRAM-ID.    "READSER",  is initial.
       AUTHOR.        Isaac Garcia Peveri.
       REMARKS.       Written in AcuCobol 7.0.0
      ******************************************************************
      * This example shows how to get a value from Arduino sensor      *
      * by calling a Tcp Serial Driver written in VB.NET               *
      * (as is a TCP driver, I am using C$SOCKET routine)              *
      ******************************************************************
      *
       WORKING-STORAGE SECTION.
       77 KEY-PRESSED PIC X.
       77 CMD-ARGS       PIC X(5)       VALUE SPACES.
       77 W-MESSAGE      PIC X(35)      VALUE SPACES.
       77 SOCKET-HANDLE  USAGE HANDLE.
       77 AGS-CONNECT    PIC 9          VALUE 3.
       77 AGS-WRITE      PIC 9          VALUE 5.
       77 AGS-READ       PIC 9          VALUE 6.
       77 AGS-FLUSH      PIC 9          VALUE 7.
      *
       SCREEN SECTION.
       01  MAIN-SCREEN.
           03 BLANK SCREEN BACKGROUND-COLOR 0 FOREGROUND-COLOR 6.
       01  FIELDS AUTO.
           03 LINE 3  COLUMN 28 HIGHLIGHT "READING SERIAL DATA (TCP)"
              COLOR 11.
           03 LINE 4 COLUMN 28 HIGHLIGHT "2023 - ISAAC GARCIA PEVERI"
              COLOR 11.
           03 LINE 6 COLUMN 02 HIGHLIGHT "Application is reading the sen
      -       "sor value from Arduino, via the TCP Server"
              COLOR 4.
           03 LINE 7 COLUMN 02 HIGHLIGHT "written by me in .NET. While t
      -       "his thread is running, it is possible to send"
              COLOR 4.
           03 LINE 8 COLUMN 02 HIGHLIGHT
              "a request to the TCP server to Control the Arduino "
              COLOR 4.
           03 LINE 11 COLUMN 20 HIGHLIGHT
              ".-------------------------------------." COLOR 8
              REVERSE-VIDEO.
           03 LINE 12 COLUMN 20 HIGHLIGHT
              "|       REALTIME SENSOR VALUE:        |" COLOR 8
              REVERSE-VIDEO.
           03 LINE 13 COLUMN 20 HIGHLIGHT
              "|                                     |" COLOR 8
              REVERSE-VIDEO.
           03 LINE 13 COLUMN 22 PIC X(35)
              USING W-MESSAGE COLOR 8 REVERSE-VIDEO.
           03 LINE 14 COLUMN 20 HIGHLIGHT
              "'-------------------------------------'" COLOR 8
              REVERSE-VIDEO.
           03 LINE 18 COLUMN 20
              HIGHLIGHT "  ENTER X: TO EXIT APPLICATION"
              COLOR 6.
           03 LINE 19 COLUMN 20
              HIGHLIGHT "  ENTER 1: TO TURN ON THE LED"
              COLOR 6.
           03 LINE 20 COLUMN 20
              HIGHLIGHT "(sends a TCP message to the server)"
              COLOR 6.
        01 KEY-INPUT.
           03 LINE 21 COLUMN 20 HIGHLIGHT "CHOICE:" COLOR 4
              REVERSE-VIDEO.
           03 LINE 21 PIC X COLUMN 35 USING KEY-PRESSED
              REVERSE-VIDEO.
           03 LINE 21 COLUMN 30
              HIGHLIGHT " THEN PRESS ENTER, TO CONFIRM"
              COLOR 4 REVERSE-VIDEO.
      *
      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       PROCEDURE DIVISION.
       MAIN.
            DISPLAY MAIN-SCREEN
            DISPLAY FIELDS

            CALL 'C$SOCKET'         USING AGS-CONNECT
                                          64000
                                          "127.0.0.1"
                                    GIVING SOCKET-HANDLE

            PERFORM THREAD WORKING-CYCLE
            PERFORM ACCEPT-KEYPRESS

            STOP RUN
            .

      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       WORKING-CYCLE.
            PERFORM UNTIL KEY-PRESSED = 'X' OR 'x'
      * Get data:
      *        1) Sending a specific request to TCP Server "GETDATA"
               CALL 'C$SOCKET'      USING AGS-WRITE
                                    SOCKET-HANDLE
                                    "GETDATA"
                                    7

               CALL 'C$SOCKET'      USING AGS-FLUSH
                                    SOCKET-HANDLE
      *
      *        2) Ask the server the data
               CALL 'C$SOCKET'      USING AGS-READ
                                    SOCKET-HANDLE
                                    CMD-ARGS
                                    5

               CALL 'C$SOCKET'      USING AGS-FLUSH
                                    SOCKET-HANDLE

               STRING "                " DELIMITED SIZE
                      CMD-ARGS(1:3)      DELIMITED SIZE
                      INTO W-MESSAGE

               DISPLAY FIELDS

               IF KEY-PRESSED = '1'
      *           1) Sending a specific request to TCP Server "SENDDATA"
                  CALL 'C$SOCKET'   USING AGS-WRITE
                                    SOCKET-HANDLE
                                    "SENDDATA:ON"
                                    11
                  CALL 'C$SOCKET'   USING AGS-FLUSH
                                    SOCKET-HANDLE

                  MOVE SPACES TO KEY-PRESSED
               END-IF

               CALL "C$SLEEP"       USING 1
            END-PERFORM
            .
      *++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*
       ACCEPT-KEYPRESS.
            PERFORM UNTIL KEY-PRESSED = 'X' or 'x'
               CALL "C$SLEEP" USING 1
               ACCEPT KEY-PRESSED ON EXCEPTION CONTINUE END-ACCEPT
            END-PERFORM
            .