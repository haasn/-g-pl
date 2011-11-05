#include <iostream>
#include <fstream>
#include <vector>
#include <algorithm>
#include <functional>
#include <locale>


using std::cerr;
using std::endl;
using std::cout;
using std::string;
using std::vector;
using std::remove;

struct property{
	string key;
	string value;
};

static inline std::string &ltrim(std::string &s) {
        s.erase(s.begin(), std::find_if(s.begin(), s.end(), std::not1(std::ptr_fun<int, int>(std::isspace))));
        return s;
}

static inline std::string &rtrim(std::string &s) {
        s.erase(std::find_if(s.rbegin(), s.rend(), std::not1(std::ptr_fun<int, int>(std::isspace))).base(), s.end());
        return s;
}

static inline std::string &trim(std::string &s) {
        return ltrim(rtrim(s));
}


int main(int argc, char* argv[]){
	if(argc != 2){
		cerr << "Usage: " << argv[0] << " [file.gpl]" << endl;
		return 1;
	}

	std::ifstream ifs(argv[1]);
	if(!ifs){
		cerr << "Can not open file " << argv[1] << ", terminating" << endl;
		return 1;
	}

	string line;
	bool inblock = false;
	vector<property> properties;



	while(std::getline(ifs, line)){
		if(inblock){
			//Parse stuff
			if(line == "filtered"){
				inblock = false;
			}else{
				size_t loc = line.find(":");
				if(loc == -1){
					cerr << "Malformed line found. Ignoring line, continuing executation." << endl;
				}else{
					struct property p;
					string key = line.substr(0, loc);
					p.key = trim(key);
					string value = line.substr(loc+1);
					p.value = trim(value);
					properties.push_back(p);
				}
			}
		}else{
			//Look for beginning blocks
			if(line == "tripfaggotry"){
				inblock = true;
			}
		}
	}

	if(inblock){
		cerr << "File is formatted properly. This program has tried to work with it." << endl;
	}

	cout << "Found " << properties.size() << " entries in " << argv[1] << endl;
	for(vector<property>::size_type i = 0; i < properties.size(); i++){
		cout << properties[i].key << ": " << properties[i].value << endl;
	}
}
