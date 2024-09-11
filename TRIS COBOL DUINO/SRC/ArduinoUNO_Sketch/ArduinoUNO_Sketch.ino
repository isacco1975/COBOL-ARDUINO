/*
 **********************************************************************
 * Name:    SendingData.ino                                           *
 * - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  *
 * Created: 09.03.2023 10:01:49                                       *
 * Author:  Isaac Garcia Peveri                                       *
 *          isacco1975gp@gmail.com                                    *
 * - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  *
 **********************************************************************
*/


#pragma region "Working-Storage"
String incomingMessage;

const int BUTTONR = 2;
const int BUTTON1 = 8;
const int BUTTON2 = 9;
const int BUTTON3 = 10;
const int BUTTON4 = 5;
const int BUTTON5 = 6;
const int BUTTON6 = 7;
const int BUTTON7 = A4;
const int BUTTON8 = 22;
const int BUTTON9 = 24;

const int GLED1 = 39;
const int GLED2 = 37;
const int GLED3 = 35;
const int GLED4 = 43;
const int GLED5 = 45;
const int GLED6 = 47;
const int GLED7 = A2;
const int GLED8 = A3;
const int GLED9 = A1;

const int RLED1 = 38;
const int RLED2 = 36;
const int RLED3 = 34;
const int RLED4 = 42;
const int RLED5 = 44;
const int RLED6 = 46;
const int RLED7 = 28;
const int RLED8 = 30;
const int RLED9 = 32;

byte buttonRValue = 0;
byte button1Value = 0;
byte button2Value = 0;
byte button3Value = 0;
byte button4Value = 0;
byte button5Value = 0;
byte button6Value = 0;
byte button7Value = 0;
byte button8Value = 0;
byte button9Value = 0;
#pragma endregion

void setup()
{  
   Serial.begin(9600);

   pinMode(BUTTONR, INPUT);

   pinMode(BUTTON1, INPUT);
   pinMode(GLED1, OUTPUT);
   pinMode(RLED1, OUTPUT);

   pinMode(BUTTON2, INPUT);
   pinMode(GLED2, OUTPUT);
   pinMode(RLED2, OUTPUT);

   pinMode(BUTTON3, INPUT);
   pinMode(GLED3, OUTPUT);
   pinMode(RLED3, OUTPUT);

   pinMode(BUTTON4, INPUT);
   pinMode(GLED4, OUTPUT);
   pinMode(RLED4, OUTPUT);

   pinMode(BUTTON5, INPUT);
   pinMode(GLED5, OUTPUT);
   pinMode(RLED5, OUTPUT);

   pinMode(BUTTON6, INPUT);
   pinMode(GLED6, OUTPUT);
   pinMode(RLED6, OUTPUT);

   pinMode(BUTTON7, INPUT);
   pinMode(GLED7, OUTPUT);
   pinMode(RLED7, OUTPUT);

   pinMode(BUTTON8, INPUT);
   pinMode(GLED8, OUTPUT);
   pinMode(RLED8, OUTPUT);

   pinMode(BUTTON9, INPUT);
   pinMode(GLED9, OUTPUT);
   pinMode(RLED9, OUTPUT);
}

///
/// Reading Loop
///
void loop()
{
   buttonRValue = digitalRead(BUTTONR); 
   button1Value = digitalRead(BUTTON1); 
   button2Value = digitalRead(BUTTON2);   
   button3Value = digitalRead(BUTTON3); 
   button4Value = digitalRead(BUTTON4);
   button5Value = digitalRead(BUTTON5); 
   button6Value = digitalRead(BUTTON6); 
   button7Value = digitalRead(BUTTON7);
   button8Value = digitalRead(BUTTON8);
   button9Value = digitalRead(BUTTON9);

/*
   Serial.println(button1Value);
   Serial.println(button2Value);
   Serial.println(button3Value);
   Serial.println(button4Value);
   Serial.println(button5Value);
   Serial.println(button6Value);
   Serial.println(button7Value);
   Serial.println(button8Value);
   Serial.println(button9Value);
*/
   if (buttonRValue == 1)
   {
      Serial.println("R");

      digitalWrite(RLED1, LOW);
      digitalWrite(RLED2, LOW);
      digitalWrite(RLED3, LOW);
      digitalWrite(RLED4, LOW);
      digitalWrite(RLED5, LOW);
      digitalWrite(RLED6, LOW);
      digitalWrite(RLED7, LOW);
      digitalWrite(RLED8, LOW);
      digitalWrite(RLED9, LOW);    

      digitalWrite(GLED1, LOW);
      digitalWrite(GLED2, LOW);
      digitalWrite(GLED3, LOW);
      digitalWrite(GLED4, LOW);
      digitalWrite(GLED5, LOW);
      digitalWrite(GLED6, LOW);
      digitalWrite(GLED7, LOW);
      digitalWrite(GLED8, LOW);
      digitalWrite(GLED9, LOW);    
   }

   if (button1Value == 1) 
   {
      Serial.println("A");
      digitalWrite(GLED1, HIGH);      
      delay(1000);
   } 

   if (button2Value == 1) 
   {
      Serial.println("B");
      digitalWrite(GLED2, HIGH);      
      delay(1000);
   } 

   if (button3Value == 1) 
   {
      Serial.println("C");
      digitalWrite(GLED3, HIGH);      
      delay(1000);
   } 

   if (button4Value == 1) 
   {
      Serial.println("D");
      digitalWrite(GLED4, HIGH);      
      delay(1000);
   } 

   if (button5Value == 1) 
   {
      Serial.println("E");
      digitalWrite(GLED5, HIGH);      
      delay(1000);
   } 

   if (button6Value == 1) 
   {
      Serial.println("F");
      digitalWrite(GLED6, HIGH);      
      delay(1000);
   } 

   if (button7Value == 1) 
   {
      Serial.println("G");
      digitalWrite(GLED7, HIGH);      
      delay(1000);
   } 

   if (button8Value == 1) 
   {
      Serial.println("H");
      digitalWrite(GLED8, HIGH);      
      delay(1000);
   } 
  
   if (button9Value == 1) 
   {
      Serial.println("I");
      digitalWrite(GLED9, HIGH);      
      delay(1000);
   } 

   incomingMessage = "";
   Serial.flush();

   if (Serial.available()) 
   {
      incomingMessage = Serial.readString();
      //Serial.println(incomingMessage);
      
      if (incomingMessage == "1")
      {
         digitalWrite(RLED1, HIGH);
      } 

      if (incomingMessage == "2")
      {
         digitalWrite(RLED2, HIGH);
      }

      if (incomingMessage == "3")
      {
         digitalWrite(RLED3, HIGH);
      }

      if (incomingMessage == "4")
      {
         digitalWrite(RLED4, HIGH);
      }

      if (incomingMessage == "5")
      {
         digitalWrite(RLED5, HIGH);
      }

      if (incomingMessage == "6")
      {
         digitalWrite(RLED6, HIGH);
      }

      if (incomingMessage == "7")
      {
         digitalWrite(RLED7, HIGH);
      }

      if (incomingMessage == "8")
      {
         digitalWrite(RLED8, HIGH);
      }

      if (incomingMessage == "9")
      {
         digitalWrite(RLED9, HIGH);
      }
   } 

   delay(500);
  
   //Avoid COBOL SOCKET to wait forever 
   Serial.println("");
}
