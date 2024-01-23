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

#include <LiquidCrystal.h>
#define backlight 10    

#pragma region "Working-Storage"
const int GAS_SENSOR = A5;   //Analog reading connected on this pin.
const int LED_PIN = 8;
int sensorValue = 0;         //This is the analog value read by the sensor.
String incomingMessage;

LiquidCrystal LCD_DISPLAY(12, 11, 5, 4, 3, 2);
#pragma endregion

void setup()
{  
   Serial.begin(9600);
   pinMode(LED_PIN, OUTPUT);
   LCD_DISPLAY.begin(16, 2);
   LCD_DISPLAY.clear();
   LCD_DISPLAY.setCursor(0, 0);
   LCD_DISPLAY.print(" Sensor Value:");
}

///
/// Reading Loop
///
void loop()
{
   sensorValue = analogRead(GAS_SENSOR);  
   DisplayValueOnLCD();
   SendValueToSerial();

   if (Serial.available()) 
   {
      incomingMessage = Serial.readString();

      if (incomingMessage == "ON") 
      {
          digitalWrite(LED_PIN, HIGH);
      }
      delay (2000);
     digitalWrite(LED_PIN, LOW);
   }

   delay(500);
}

///
/// Displaying sensor value on LCD Display
///
void DisplayValueOnLCD()
{  
   if (sensorValue >= 1000) 
   {
     LCD_DISPLAY.setCursor(3, 1);
     LCD_DISPLAY.print("  ");
   }
   
   if (sensorValue < 1000)
   {
     LCD_DISPLAY.setCursor(4, 1);      
     LCD_DISPLAY.print("  ");
   }
   
   if (sensorValue < 100)
   {
     LCD_DISPLAY.setCursor(5, 1);
     LCD_DISPLAY.print("  ");
   }
   
   if (sensorValue < 10)
   {
     LCD_DISPLAY.setCursor(6, 1);
     LCD_DISPLAY.print("  ");
   }
     
   LCD_DISPLAY.print(sensorValue, DEC);
   Serial.println(sensorValue, DEC);      
}

///
/// Sending to the Serial port
///
void SendValueToSerial()
{
   Serial.println(sensorValue, DEC);
}
