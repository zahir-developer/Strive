import { Injectable } from '@angular/core';

// @Injectable({
//     providedIn: 'root'
// })

export class CodeValueService {
    codeValue; // = [];
    constructor() { }

    getCodeValueByType(type) {
        const  codeValue = localStorage.getItem('codeValue');
        const parseCodeValue = JSON.parse(codeValue);
        const selectedCodeValue = parseCodeValue.filter(item => item.Category === type);
        return selectedCodeValue;
    }

}
