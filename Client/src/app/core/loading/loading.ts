import { Component, computed } from '@angular/core';
import {CommonModule} from '@angular/common';
import {LoadingService} from '../services/loading.service'

@Component({
  selector: 'app-loading',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loading.html',
  styleUrl: './loading.scss',
})
export class Loading {
  constructor(private loadingService: LoadingService) { }
  isLoading = computed(() => this.loadingService.isLoading())
}
