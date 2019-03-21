import { Component, OnInit } from '@angular/core';

import { ModalService } from '../modal.service';

@Component({
    moduleId: module.id,
    templateUrl: 'punch-duration-modal.html'
})

export class PunchDurationComponent implements OnInit {
    private bodyText: string;

    constructor(private modalService: ModalService) {
    }

    ngOnInit() {
        this.bodyText = 'This text can be updated in modal 1';
    }

    openModal(id: string){
        this.modalService.open(id);
    }

    closeModal(id: string){
        this.modalService.close(id);
    }
}
