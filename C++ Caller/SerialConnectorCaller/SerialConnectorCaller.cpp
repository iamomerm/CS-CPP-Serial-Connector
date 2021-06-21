#include <iostream>
#include <fstream>       
#include <Windows.h>
#include <system_error>
#import "C:\\SerialConnector\\SerialConnector.tlb"

int main()
{
    // Validate TLB File
    std::fstream fileStream;  
    fileStream.open("C:\\SerialConnector\\SerialConnector.tlb");
    if (fileStream.fail()) 
    {
        std::cout << "C:\\SerialConnector\\SerialConnector.tlb File Not Found!" << std::endl;
        return -1;
    }

    try
    {
        CoInitialize(NULL);

        SerialConnector::SCInterfacePtr SCPtr;
        HRESULT hRes = SCPtr.CreateInstance(__uuidof(SerialConnector::SC));

        if (!(SUCCEEDED(hRes)))
        {
            std::cout << "Failed to Create SerialConnector Instance!" << std::endl;
            std::cout << "HRESULT Code: " << HRESULT_CODE(hRes) << std::endl;
            std::cout << "Error Message: " << std::system_category().message(hRes) << std::endl;
            return -1;
        }

        std::string Port;
        std::cout << "Enter Com Port: ";
        std::cin >> Port;
        
        // Connect
        int status = SCPtr->Connect(Port.c_str(), 9600);

        // Connection Succeeded
        if (status == 0)
        {
            std::cout << "Connection Succeeded!" << std::endl;

            // Write
            int status = SCPtr->Write("HELLO WORLD");
        }

        // Connection Failed
        else
        {
            std::cout << "Connection Failed!" << std::endl;
        }

        CoUninitialize();
    }

    catch (std::exception Ex)
    {
        std::cout << Ex.what() << std::endl;
    }

    return 0;
}
